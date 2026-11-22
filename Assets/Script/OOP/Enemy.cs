using UnityEngine;
using UnityEngine.Events;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Stats")]
    [SerializeField] protected int hp = 100;
    public float ms = 2.5f; // Kecepatan jalan
    [SerializeField] protected ZombieConfig config;

    protected Transform player;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    [Header("State Machine")]
    [SerializeField] protected float JarakDeteksi = 6f;  // Jarak mulai mengejar (6 langkah)
    [SerializeField] protected float JarakSerang = 1.2f; // Jarak meledak menyerang

    public static event Action<Enemy> OnZombieMati;

    [Header("Event Kematian")]
    [SerializeField] private UnityEvent onZombieMatiVisual;

    protected StateZombie state = StateZombie.IDLE;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (config != null)
        {
            TerapkanConfig();
        }

        // Pengaman angka
        if (ms <= 0) ms = 2.5f;
        if (JarakDeteksi <= 0) JarakDeteksi = 6f;
        if (JarakSerang <= 0) JarakSerang = 1.2f;

        CariPlayer();
    }

    void CariPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    protected virtual void TerapkanConfig()
    {
        hp = config.hp;
        ms = config.kecepatan;
        JarakDeteksi = config.jarakDeteksi;
        JarakSerang = config.jarakSerang;
    }

    protected virtual void Update()
    {
        // 1. Berhenti jika game sudah Game Over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            if (animator != null) animator.SetFloat("Speed", 0);
            return;
        }

        // 2. Cari player jika belum ketemu
        if (player == null)
        {
            CariPlayer();
            return;
        }

        // 3. Cek jarak ke player
        PeriksaTransisi();

        // 4. Jalankan perilaku sesuai State
        switch (state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= JarakSerang)
        {
            state = StateZombie.ATTACK; // Dekat sekali -> Serang!
        }
        else if (jarak <= JarakDeteksi)
        {
            state = StateZombie.CHASE;  // Dalam jarak pandang -> Kejar!
        }
        else
        {
            state = StateZombie.IDLE;   // Di luar jarak pandang -> Berhenti diam!
        }
    }

    public virtual void Kejar()
    {
        if (player == null) return;

        // Hitung arah gerak ke player
        Vector2 arah = (player.position - transform.position).normalized;

        // Kirim arah ke Blend Tree 4 arah musuh
        if (animator != null)
        {
            animator.SetFloat("Horizontal", arah.x);
            animator.SetFloat("Vertical", arah.y);
        }

        // Gerakkan fisik musuh
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        // Dioverride oleh EnemyKamikaze
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        OnZombieMati?.Invoke(this);
        onZombieMatiVisual?.Invoke();
        Destroy(gameObject);
    }

    protected virtual void PerilakuIdle()
    {
        // Diam saat pemain kabur menjauh
        if (animator != null)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    protected virtual void PerilakuPatrol() { }

    protected virtual void PerilakuChase()
    {
        // Aktifkan animasi jalan & kejar
        if (animator != null)
        {
            animator.SetFloat("Speed", 1);
        }
        Kejar();
    }

    protected virtual void PerilakuAttack()
    {
        Serang();
    }
}