using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelSpawner : EditorWindow
{
    private string levelName;
    private GameObject cubeToSpawn;
    private GameObject player;
    private GameObject levelExit;
    private GameObject sideForLevelExit;
    [MenuItem("Tools/Level Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelSpawner));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Level", EditorStyles.boldLabel);

        levelName = EditorGUILayout.TextField("Level Name", levelName);
        cubeToSpawn = EditorGUILayout.ObjectField("Cube to Spawn", cubeToSpawn, typeof(GameObject), false) as GameObject;
        player = EditorGUILayout.ObjectField("Player", player, typeof(GameObject), false) as GameObject;
        levelExit = EditorGUILayout.ObjectField("Level Exit", levelExit, typeof(GameObject), false) as GameObject;
        sideForLevelExit = EditorGUILayout.ObjectField("Side for Level Exit", sideForLevelExit, typeof(GameObject), false) as GameObject;
        if (GUILayout.Button("Spawn Level"))
        {
            SpawnLevel();
        }

    }

    private void SpawnLevel()
    {
        if (cubeToSpawn == null)
        {
            Debug.Log("Error: Please assign cube to be spawned");
            return;
        }
        if (levelName == string.Empty)
        {
            Debug.Log("Error: Please enter a level name");
            return;
        }

        Vector3 levelSpawnPos = Vector3.zero;

        GameObject levelObject = Instantiate(cubeToSpawn, levelSpawnPos, Quaternion.identity);
        GameObject playerObject = Instantiate(player, Vector3.zero, Quaternion.identity);
        //playerObject.transform.FindChild()
       
        levelObject.name = levelName;
 

    }
}
