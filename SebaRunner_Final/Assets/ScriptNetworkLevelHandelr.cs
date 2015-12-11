using UnityEngine;
using System.Collections;

public class ScriptNetworkLevelHandelr : MonoBehaviour {

    ScriptSebaRunnerNetworkManager manager;
    ScriptLevelSelection gameSetup;
    public GameObject clientBlock;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("NetworkManager").GetComponent<ScriptSebaRunnerNetworkManager>();
        gameSetup = GameObject.Find("GameSetup").GetComponent<ScriptLevelSelection>();
        if(manager.startedGameFrom == Origin.CLIENT)
        {
            clientBlock.SetActive(true);
        }

	}
    
    public void SendLevel()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in allPlayers)
        {
            player.GetComponent<ScriptGetLevelFromHost>().SetLevel(gameSetup.selectedLevel);
        }
    }
}
