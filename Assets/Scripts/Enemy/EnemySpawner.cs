using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 2;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;
    public bool isSpawning = false;

    public float timer = 0;

    void Start()
    {
        Assert.IsNotNull(spawnedEnemy, "Spawned Enemy prefab has not been assigned in the inspector");
        spawnCount = defaultSpawnCount;
    }

    void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;
            CheckSpawnConditions();
        }
    }

    private void CheckSpawnConditions()
    {
        if (timer >= spawnInterval)
        {
            if (spawnCount > 0)
            {
                CreateEnemy();
                spawnCount--;
            }
            timer = 0;
        }
    }

    private void CreateEnemy()
    {
        Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
    }

    public void RecordKill()
    {
        totalKill++;
        totalKillWave++;
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            spawnCount += multiplierIncreaseCount;
            totalKillWave = 0; 
        }
    }

    public void ResetSpawning()
    {
        spawnCount = defaultSpawnCount;
    }

    public void InitializeTimer()
    {
        timer = spawnInterval - 0.1f;
    }
}
