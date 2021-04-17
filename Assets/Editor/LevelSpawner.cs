using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelSpawner : EditorWindow
{
    private string cubeToSpawn;
    private GameObject player;
    private GameObject levelManager;
    private string[] cubeOptions = { "BlankLevelCube", "MoveOnly", "RotateOnly", "Move+Rotate", "TutorialCube" };
    private string[] levelExitOptions = { "zxFront", "xyBack", "yzFront", "xyFront", "yzBack", "zxBack" };
    private int cubeDropdownIndex;
    private int levelExitIndex;
    [MenuItem("Tools/Level Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelSpawner));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Level", EditorStyles.boldLabel);

        //cubeToSpawn = EditorGUILayout.ObjectField("Cube to Spawn", cubeToSpawn, typeof(GameObject), false) as GameObject;
        //player = EditorGUILayout.ObjectField("Player", player, typeof(GameObject), false) as GameObject;
        //levelManager = EditorGUILayout.ObjectField("Level Manager", levelManager, typeof(GameObject), false) as GameObject;
        cubeDropdownIndex = EditorGUILayout.Popup("Cube to Spawn", cubeDropdownIndex, cubeOptions);
        levelExitIndex = EditorGUILayout.Popup("Side of Cube for Level Exit", levelExitIndex, levelExitOptions);
        if (GUILayout.Button("Spawn Level"))
        {
            SpawnLevel();
        }

    }

    private void SpawnLevel()
    {
        switch(cubeDropdownIndex)
        {
            case 0:
                cubeToSpawn = "Assets/Prefabs/LevelCubes/BlankLevelCube.prefab";
                break;
            case 1:
                cubeToSpawn = "Assets/Prefabs/LevelCubes/Opening Levels/MoveOnly.prefab";
                break;
            case 2:
                cubeToSpawn = "Assets/Prefabs/LevelCubes/Opening Levels/RotateOnly.prefab";
                break;
            case 3:
                cubeToSpawn = "Assets/Prefabs/LevelCubes/Opening Levels/Move+Rotate.prefab";
                break;
            case 4:
                cubeToSpawn = "Assets/Prefabs/LevelCubes/Opening Levels/TutorialCube.prefab";
                break;
            default:
                cubeToSpawn = "Assets/Prefabs/LevelCubes/BlankLevelCube.prefab";
                break;
        }
        GameObject cubePrefab = PrefabUtility.LoadPrefabContents(cubeToSpawn);
        GameObject managerPrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Level Manager.prefab");
        GameObject playerPrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Player.prefab");
        GameObject playerObject = Instantiate(playerPrefab);
        GameObject managerObject = Instantiate(managerPrefab);
        GameObject cubeObject = Instantiate(cubePrefab);
        managerObject.name = "Level Manager";
        playerObject.name = "Player";
        //playerObject.transform.FindChild()
        GameObject LevelExit = GameObject.Find("LevelExit");
        if (LevelExit != null)
        {
            LevelExit.transform.parent = cubeObject.transform.GetChild(levelExitIndex);
            LevelExit.transform.localPosition = Vector3.zero;
            LevelExit.name = "LevelExit";
        }
        else
        {
            GameObject LevelExitPrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/LevelExit.prefab");
            LevelExit = Instantiate(LevelExitPrefab, cubeObject.transform.GetChild(levelExitIndex));
            LevelExit.transform.localPosition = Vector3.zero;
            LevelExit.name = "LevelExit";


        }



    }
}
