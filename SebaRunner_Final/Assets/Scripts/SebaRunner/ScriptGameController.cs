using UnityEngine;
using System.Collections;

public class ScriptGameController : MonoBehaviour {

    [Header("Visual Options")]
    public GameObject sparks;
    public bool showCursor = false;

    void Start()
    {
        Cursor.visible = showCursor;

        if(sparks == null)
        {
            Debug.Log("No spark object assigned to the game controller.");
            Debug.Log("Please assign a spark effect in the script game controller.");
        }
    }
}
