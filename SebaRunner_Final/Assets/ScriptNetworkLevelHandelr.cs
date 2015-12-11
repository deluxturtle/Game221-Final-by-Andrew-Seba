using UnityEngine;
using System.Collections;

public class ScriptNetworkLevelHandelr : MonoBehaviour {

    ScriptSebaRunnerNetworkManager manager;
    ScriptLevelSelection gameSetup;
    public GameObject clientBlock;

	// Use this for initialization
	void Start () {
        try
        {
            manager = GameObject.Find("NetworkManager").GetComponent<ScriptSebaRunnerNetworkManager>();
            if (manager.startedGameFrom == Origin.CLIENT)
            {
                clientBlock.SetActive(true);
            }
        }
        catch
        {
            Debug.Log("No Network Manager");
        }
        finally
        {
            try
            {
                gameSetup = GameObject.Find("GameSetup").GetComponent<ScriptLevelSelection>();
            }
            catch
            {
                Debug.Log("Couldn't Find GameSetup Game object.");
            }
        }

	}
}
