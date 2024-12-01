using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextDisplay : MonoBehaviour
{
    // Variables for game state
    public int health = 0;
    public int points = 0;
    public int wave = 1;
    public int enemies = 0;

    // UI Labels
    private Label labelHealth;
    private Label labelPoints;
    private Label labelWave;
    private Label labelEnemies;

    void Start()
    {
        // Access UI elements via UIDocument
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Find the labels in the UI
        labelHealth = root.Q<Label>("Health");
        labelPoints = root.Q<Label>("Points");
        labelWave = root.Q<Label>("Wave");
        labelEnemies = root.Q<Label>("EnemiesLeft");
    }

    void Update()
    {
        CombatManager combatManager = FindObjectOfType<CombatManager>();
        if (combatManager != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            health = player.GetComponent<HealthComponent>().GetHealth();
            points = combatManager.points;
            wave = combatManager.waveNumber;
            enemies = combatManager.totalEnemies;

            labelHealth.text = "Health: " + health;
            labelPoints.text = "Points: " + points;
            labelWave.text = "Wave: " + wave;
            labelEnemies.text = "enemies: " + enemies;
        }
    }
}
