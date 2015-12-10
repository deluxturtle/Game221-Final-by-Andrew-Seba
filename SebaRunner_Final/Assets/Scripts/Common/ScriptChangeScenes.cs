using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Changes Scenes to the main ones.
/// </summary>
public class ScriptChangeScenes : MonoBehaviour {

	public void _LoadMain()
    {
        Application.LoadLevel("MainMenu");
    }

    public void _LoadCredits()
    {
        Application.LoadLevel("Credits");
    }

    public void _LoadGunSelect()
    {
        Application.LoadLevel("SceneGunSelect");
    }

    public void _LoadLevelSelect()
    {
        Application.LoadLevel("SceneLevelSelect");
    }
}
