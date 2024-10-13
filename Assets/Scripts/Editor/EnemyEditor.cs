using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Enemy enemy = (Enemy)target;

       
        DrawDefaultInspector();

      
        if (enemy.EnemyType == EnemyType.Square)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Wave Settings", EditorStyles.boldLabel);

            enemy.Frequency = EditorGUILayout.FloatField("Frequency", enemy.Frequency);
            enemy.Amplitude = EditorGUILayout.FloatField("Amplitude", enemy.Amplitude);
        }

      
        if (GUI.changed)
        {
            EditorUtility.SetDirty(enemy);
        }
    }
}
