using UnityEngine;
using UnityEditor;

public class EnemySpawnerTool : EditorWindow
{
    // Fields for the tool
    private GameObject enemyPrefab;  // Enemy prefab to instantiate
    private int enemyCount = 10;     // Number of enemies to spawn
    private string[] enemyTypes = new string[] { "Normal", "Fast", "Strong" }; // Enemy types
    private int selectedEnemyType = 0;
    private string[] shapes = new string[] { "Square", "Circle", "Triangle" }; // Shape options
    private int selectedShape = 0;
    private bool applySpeed = false; // Toggle for applying speed boost

    private float spacing = 2f;      // Spacing between enemies

    // Add menu item to open the window
    [MenuItem("Tools/Enemy Spawner")]
    public static void ShowWindow()
    {
        GetWindow<EnemySpawnerTool>("Enemy Spawner Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Enemy Spawner Tool", EditorStyles.boldLabel);

        // Select enemy prefab
        enemyPrefab = (GameObject)EditorGUILayout.ObjectField("Enemy Prefab", enemyPrefab, typeof(GameObject), false);

        // Number of enemies to spawn
        enemyCount = EditorGUILayout.IntField("Enemy Count", enemyCount);

        // Enemy type selection
        selectedEnemyType = EditorGUILayout.Popup("Enemy Type", selectedEnemyType, enemyTypes);

        // Shape selection
        selectedShape = EditorGUILayout.Popup("Formation Shape", selectedShape, shapes);

        // Spacing between enemies
        spacing = EditorGUILayout.FloatField("Spacing", spacing);

        // Option to apply speed boost
        applySpeed = EditorGUILayout.Toggle("Apply Speed Modifier", applySpeed);

        // Button to generate enemies
        if (GUILayout.Button("Spawn Enemies"))
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("No enemy prefab selected!");
            return;
        }

        // Determine the spawning shape
        switch (shapes[selectedShape])
        {
            case "Square":
                SpawnInSquare();
                break;
            case "Circle":
                SpawnInCircle();
                break;
            case "Triangle":
                SpawnInTriangle();
                break;
        }
    }

    private void SpawnInSquare()
    {
        int rows = Mathf.CeilToInt(Mathf.Sqrt(enemyCount));  // Rows and columns for square formation
        for (int i = 0; i < enemyCount; i++)
        {
            float x = (i % rows) * spacing;
            float y = (i / rows) * spacing;

            Vector3 position = new Vector3(x, 0, y);
            SpawnEnemy(position);
        }
    }

    private void SpawnInCircle()
    {
        float radius = Mathf.Sqrt(enemyCount) * spacing;
        for (int i = 0; i < enemyCount; i++)
        {
            float angle = i * Mathf.PI * 2 / enemyCount;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 position = new Vector3(x, 0, y);
            SpawnEnemy(position);
        }
    }

    private void SpawnInTriangle()
    {
        int index = 0;
        for (int row = 1; index < enemyCount; row++)
        {
            for (int col = 0; col < row && index < enemyCount; col++)
            {
                float x = col * spacing - row * spacing * 0.5f;
                float y = row * spacing;

                Vector3 position = new Vector3(x, 0, y);
                SpawnEnemy(position);
                index++;
            }
        }
    }

    private void SpawnEnemy(Vector3 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);

        switch (enemyTypes[selectedEnemyType])
        {
            case "Normal":
                enemy.name = "Normal Enemy";
                break;
            case "Fast":
                enemy.name = "Fast Enemy";

                break;
            case "Strong":
                enemy.name = "Strong Enemy";
                // Apply specific logic for strong enemies
                break;
        }

        // Make the spawned enemies children of the tool for easier organization
        enemy.transform.SetParent(null);  // Or you can assign a custom parent object
    }
}
