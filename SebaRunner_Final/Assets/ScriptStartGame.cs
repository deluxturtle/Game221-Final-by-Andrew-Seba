using UnityEngine;
using System.Collections;

public class ScriptStartGame : MonoBehaviour {

    ScriptLevelSelection gameInfo;
	// Use this for initialization
	void Start () {
        gameInfo = GameObject.Find("GameSetup").GetComponent<ScriptLevelSelection>();
	}
	

    public void _StartGame()
    {
        Application.LoadLevel(gameInfo.selectedLevel);
    }
}
