using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ScriptSetupNetworkManager : MonoBehaviour {

    public ScriptSebaRunnerNetworkManager manager;

    public void UpdateIP(Object message)
    {
        Text textObj = ((GameObject)message).GetComponent<Text>();
        Debug.Log("lol: " + textObj.text);
        if (textObj.text == "")
            manager.networkAddress = "localHost";
        else
            manager.networkAddress = textObj.text;
        Debug.Log("Updating address to..." + textObj.text);
    }

    public void UpdatePort(Object message)
    {
        Text textObj = ((GameObject)message).GetComponent<Text>();
        int newPort;

        if (int.TryParse(textObj.text, out newPort))
            manager.networkPort = newPort;
        else
            manager.networkPort = 7777;

        Debug.Log("Updating port to..." + newPort.ToString());
    }

    public void _StartHost()
    {
        manager.startedGameFrom = Origin.HOST;
        manager.StartHost();
    }

    public void _JoinGame()
    {
        manager.startedGameFrom = Origin.CLIENT;
        manager.StartClient();
    }
}
