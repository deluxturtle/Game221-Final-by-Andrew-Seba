using UnityEngine;
using UnityEditor;

/*
 * @author Mike Dobson
 * */

[CustomEditor(typeof(ScriptEngine))]
public class EngineEditor :  Editor
{
	
	ScriptEngine engineScript;
    public static EngineWindowEditor window;

	public override void OnInspectorGUI()
	{
        if(GUILayout.Button("Editor"))
        {
            window = (EngineWindowEditor)EditorWindow.GetWindow(typeof(EngineWindowEditor), true, "Engine Edtior", true);
        }
    }
}
