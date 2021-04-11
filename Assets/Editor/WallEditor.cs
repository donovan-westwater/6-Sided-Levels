using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wall))]
public class WallEditor : Editor
{
    private string[] wallOptions = { "Wall", "RotateWall", "PhaseWall" };
    private int index;
    public override void OnInspectorGUI()
    {
        Wall wall = (Wall)target;
        //base.OnInspectorGUI();
        index = EditorGUILayout.Popup("Wall Type", index, wallOptions);
        if (GUILayout.Button("Change Wall Type"))
        {
            wall.ChangeWallType(index);
        }
    }

}
