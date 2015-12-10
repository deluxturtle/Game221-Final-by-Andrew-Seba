using UnityEngine;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Gets the Gun from the previous scene and holds the sound for the gun.
/// </summary>
public class ScriptPlayerSetup : MonoBehaviour {

    ScriptLevelSelection gameInfo;
    public ScriptGun myGun;
    public AudioClip gunShotSound;
	// Use this for initialization
	void Awake () {
        try
        {
            gameInfo = GameObject.Find("GameSetup").GetComponent<ScriptLevelSelection>();
            myGun = gameInfo.GetGun();
            if (gameInfo.myGun != null)
                myGun = gameInfo.GetGun();
        }
        catch
        {
            Debug.Log("LoadSceneFrom Main Menu!!");
        }
    }
}
