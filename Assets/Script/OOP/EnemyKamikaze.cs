using UnityEngine;

public class EnemyKamikaze : Enemy
{
    [Header("Kamikaze Settings")]
    [SerializeField] private int damageSerangan = 25;

    protected override void Start()
    {
        base.Start();
        if (config != null)
        {
            damageSerangan = config.damage;
        }
    }
    public override void Serang()
    {
        base.Serang();
        LedakkanDiriKePlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LedakkanDiriKePlayer();
        }
    }

    void LedakkanDiriKePlayer()
    {
        if (player != null)
        {
            IDamageable target = player.GetComponent<IDamageable>();
            if (target != null)
            {
                target.KenaDamage(damageSerangan);
            }
            else
            {
                PlayerHealth ph = player.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(damageSerangan);
                }
            }
          }
        Mati();
    }
}