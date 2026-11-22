using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Reference")]
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;

        // Pengaman: Jika lupa drag slider di Inspector, otomatis dicari
        if (healthSlider == null)
        {
            GameObject barObj = GameObject.Find("HealthBar");
            if (barObj != null) healthSlider = barObj.GetComponent<Slider>();
        }

        UpdateHealthUI();
    }

    // FUNGSI DARI INTERFACE IDamageable (Panggilan dari Musuh)
    public void KenaDamage(int jumlah)
    {
        TakeDamage(jumlah);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        Debug.Log(" Player menerima damage: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

        void Die()
    {
        Debug.Log("Game Over!");

        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}