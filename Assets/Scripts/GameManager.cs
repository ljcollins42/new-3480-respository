using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject enemyTwoPrefab;
    public GameObject enemyExplosionPrefab;

    public TextMeshProUGUI livesText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6f;
        score = 0;
        if (enemyExplosionPrefab == null && enemyOnePrefab != null)
        {
            Enemy enemyOneScript = enemyOnePrefab.GetComponent<Enemy>();
            if (enemyOneScript != null && enemyOneScript.explosionPrefab != null)
            {
                enemyExplosionPrefab = enemyOneScript.explosionPrefab;
            }
        }
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("SecondEnemy",2,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0);
        SpawnEnemy(enemyOnePrefab, spawnPosition, Quaternion.Euler(180, 0, 0));
    }

    void SecondEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 1.5f, verticalScreenSize, 7);
        SpawnEnemy(enemyTwoPrefab, spawnPosition, Quaternion.Euler(0, 1800, 0));
    }

    GameObject SpawnEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject enemy = Instantiate(prefab, position, rotation);
        ConfigureEnemy(enemy);
        return enemy;
    }

    void ConfigureEnemy(GameObject enemy)
    {
        if (enemy == null)
        {
            return;
        }

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript == null)
        {
            enemyScript = enemy.AddComponent<Enemy>();
        }

        if (enemyScript.explosionPrefab == null)
        {
            enemyScript.explosionPrefab = enemyExplosionPrefab;
        }
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
}
