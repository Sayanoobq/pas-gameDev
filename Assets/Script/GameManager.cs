using UnityEngine;
using UnityEngine.SceneManagement; // Wajib untuk fungsi restart game

public class GameManager : MonoBehaviour
{
    // Singleton agar bisa dipanggil dari script mana saja dengan GameManager.Instance
    public static GameManager Instance;

    [Header("Game States")]
    public bool isGameOver = false;

    [Header("UI Panels")]
    public GameObject gameOverPanel; // Panel Game Over

    void Awake()
    {
        // Setup Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Pastikan panel Game Over tersembunyi di awal game
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

        // Dipanggil saat Player mati
    public void TriggerGameOver()
    {
        isGameOver = true;

        // Munculkan layar Game Over di layar game (tanpa spam di Console)
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // Fungsi untuk tombol "Restart" di layar Game Over
    public void RestartGame()
    {
        // Muat ulang scene dari awal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}