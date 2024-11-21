using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    void Start()
    {
        SetupSpawners();
    }

    void Update()
    {
        ManageWaveProgression();
    }

    private void SetupSpawners()
    {
        foreach (var spawner in enemySpawners)
        {
            ConfigureSpawner(spawner, true);
            if (spawner.isSpawning)
            {
                totalEnemies += spawner.defaultSpawnCount;
            }
        }
    }

    private void ProcessNextWave()
    {
        waveNumber++;
        foreach (var spawner in enemySpawners)
        {
            ConfigureSpawner(spawner, true);
            spawner.ResetSpawning();
            if (spawner.isSpawning)
            {
                totalEnemies += spawner.spawnCount;
            }
            spawner.InitializeTimer();
        }
        timer = 0;  // Reset the timer for the new wave
    }

    public void ConfigureSpawner(EnemySpawner spawner, bool activate)
    {
        if (spawner.spawnedEnemy != null && spawner.spawnedEnemy.level <= waveNumber)
        {
            spawner.isSpawning = activate;
        }
    }

    private void ManageWaveProgression()
    {
        if (totalEnemies == 0)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                ProcessNextWave();
            }
        }
    }
}
