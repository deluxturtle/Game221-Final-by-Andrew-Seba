using UnityEngine;
using System.Collections;
using UnityEditor;


/// <summary>
/// @Author: Andrew Seba
/// @Description: Opens the gun creator window.
/// </summary>
public class ScriptGunCreator : MonoBehaviour {

    [MenuItem("Rail Tools/Gun Creator")]
	public static void OpenGunCreator()
    {
        ScriptGunCreatorWindow window = (ScriptGunCreatorWindow)EditorWindow.GetWindow(typeof(ScriptGunCreatorWindow));
        window.Show();
    }
}
