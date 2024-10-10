using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Temel Inspector görünümünü çiz
        DrawDefaultInspector();

        // Hedef script'i al (Enemy)
        Enemy enemy = (Enemy)target;

        // Eğer düşman tipi Square ise, dalga ayarlarını göster
        if (enemy.EnemyType == EnemyType.Square)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Wawe Settings", EditorStyles.boldLabel);
            enemy.Frequency = EditorGUILayout.FloatField("Frequency", enemy.Frequency);
            enemy.Amplitude = EditorGUILayout.FloatField("Amplitude", enemy.Amplitude);
        }
    }
}
