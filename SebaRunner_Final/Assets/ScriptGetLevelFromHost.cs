using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ScriptGetLevelFromHost : NetworkBehaviour {

    string chosenLevel = "";

    [Command]
    public void SetLevel(string pLevel)
    {
        chosenLevel = pLevel;
    }
}
