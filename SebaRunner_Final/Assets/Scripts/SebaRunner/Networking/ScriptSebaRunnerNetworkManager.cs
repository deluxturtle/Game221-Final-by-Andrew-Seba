using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public enum Origin
{
    HOST,
    CLIENT
}

public class ScriptSebaRunnerNetworkManager : NetworkManager {

    public Origin startedGameFrom;
}
