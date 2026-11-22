using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f; // Jeda cek spawn (detik)

    [Header("Batas Musuh")]
    public int maxEnemies = 3; // MAKSIMAL MUSUH YANG BOLEH HIDUP BERSAMAAN

    // Menyimpan daftar musuh yang sedang aktif hidup di map
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (GameManager.Instance == null || !GameManager.Instance.isGameOver)
        {
            // 1. Bersihkan daftar dari musuh yang SUDAH HANCUR/MATI
            activeEnemies.RemoveAll(musuh => musuh == null);

            // 2. HANYA SPAWN jika jumlah musuh hidup MASIH KURANG dari batas maksimal
            if (activeEnemies.Count < maxEnemies)
            {
                if (enemyPrefab != null && spawnPoints.Length > 0)
                {
                    // Pilih salah satu titik spawn di map secara acak
                    int randomIndex = Random.Range(0, spawnPoints.Length);
                    GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);

                    // Masukkan musuh baru ke daftar
                    activeEnemies.Add(newEnemy);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}