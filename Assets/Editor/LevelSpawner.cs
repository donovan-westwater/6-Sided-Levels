using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelSpawner : EditorWindow
{
    private GameObject cubeToSpawn;
    private GameObject player;
    private GameObject levelManager;
    private string[] cubeOptions = { "Blank Level", "TestCube", "Move Only", "Rotate Only", "Move + Rotate", "TutorialCube" };
   // private int dropdownIndex;
    [MenuItem("Tools/Level Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelSpawner));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Level", EditorStyles.boldLabel);

        cubeToSpawn = EditorGUILayout.ObjectField("Cube to Spawn", cubeToSpawn, typeof(GameObject), false) as GameObject;
        player = EditorGUILayout.ObjectField("Player", player, typeof(GameObject), false) as GameObject;
        levelManager = EditorGUILayout.ObjectField("Level Manager", levelManager, typeof(GameObject), false) as GameObject;
        //dropdownIndex = EditorGUILayout.Popup("Cube to Spawn", dropdownIndex, cubeOptions);
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

        Vector3 levelSpawnPos = Vector3.zero;
       
        GameObject manager = Instantiate(levelManager);
        GameObject playerObject = Instantiate(player);
        GameObject levelObject = Instantiate(cubeToSpawn);
        playerObject.name = "Player";
        //playerObject.transform.FindChild()



    }
}
