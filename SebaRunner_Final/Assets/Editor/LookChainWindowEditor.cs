using UnityEngine;
using UnityEditor;

//Andrew
public enum ChainType{
    MOVEMENT,
    FACING
}

/// <summary>
/// @author Mike Dobson
/// </summary>

public class LookChainWindowEditor : EditorWindow {

    float[] lockTimes;
    float[] rotationSpeed;
    GameObject[] targets;

    static ScriptEngine engine;
    static int facingFocus = 0;

    static ChainType chainType;
    public static LookChainWindowEditor window;

    //Added the type parameter
    public static void Init(int FacingFocus, ScriptEngine Engine, ChainType pChainType)
    {
        chainType = pChainType;
        Debug.Log(FacingFocus);
        window = (LookChainWindowEditor)EditorWindow.GetWindow(typeof(LookChainWindowEditor), true, "Chain Editor", true);
        facingFocus = FacingFocus;
        Debug.Log(facingFocus);
        engine = Engine;
        if(engine == null)
        {
            Debug.LogError("No Engine");
        }
        window.Show();

    }

    void OnFocus()
    {
        //engine = GameObject.FindWithTag("Player").GetComponent<ScriptEngine>();
        switch (chainType)
        {
            case ChainType.MOVEMENT:
                lockTimes = engine.movements[facingFocus].lockTimes;
                rotationSpeed = engine.movements[facingFocus].rotationSpeed;
                targets = engine.movements[facingFocus].targets;
                break;
            case ChainType.FACING:
                lockTimes = engine.facings[facingFocus].lockTimes;
                rotationSpeed = engine.facings[facingFocus].rotationSpeed;
                targets = engine.facings[facingFocus].targets;
                break;
        }

        //minimum size for the display
        minSize = new Vector2(250, 300);

    }

    //on the editor Window
    void OnGUI()
    {
        //local variables
        Rect windowDisplay;
        float displayHeight = 17f;
        float displayHeightDif = 20f;
        float offsetX = 5;
        float offsetY = 5;

        for (int i = 0; i < targets.Length; i++ )
        {
            windowDisplay = new Rect(offsetX, offsetY, 100f, displayHeight);
            offsetX +=10;
            offsetY += displayHeightDif;
            EditorGUI.LabelField(windowDisplay, "Target " + (i + 1));
            windowDisplay = new Rect(offsetX, offsetY, 100f, displayHeight);
            offsetX += 100f;
            EditorGUI.LabelField(windowDisplay, "Look at Target");
            windowDisplay = new Rect(offsetX, offsetY, 100f, displayHeight);
            offsetX = 15f;
            offsetY += displayHeightDif;
            targets[i] = (GameObject)EditorGUI.ObjectField(windowDisplay, targets[i], typeof(GameObject), true);
            windowDisplay = new Rect(offsetX, offsetY, 150f, displayHeight);
            offsetX += 150f;
            EditorGUI.LabelField(windowDisplay, "Rotate to target over");
            windowDisplay = new Rect(offsetX, offsetY, 50f, displayHeight);
            offsetX += 50f;
            rotationSpeed[i] = EditorGUI.FloatField(windowDisplay, rotationSpeed[i]);
            windowDisplay = new Rect(offsetX, offsetY, 50f, displayHeight);
            offsetX = 15f;
            offsetY += displayHeightDif;
            EditorGUI.LabelField(windowDisplay, "secs");
            windowDisplay = new Rect(offsetX, offsetY, 150f, displayHeight);
            offsetX += 150f;
            EditorGUI.LabelField(windowDisplay, "Lock to target for");
            windowDisplay = new Rect(offsetX, offsetY, 50f, displayHeight);
            offsetX += 50f;
            lockTimes[i] = EditorGUI.FloatField(windowDisplay, lockTimes[i]);
            windowDisplay = new Rect(offsetX, offsetY, 50f, displayHeight);
            offsetY += displayHeightDif;
            offsetX = 5f;
            EditorGUI.LabelField(windowDisplay, "secs");
        }
        offsetY += displayHeightDif;
        windowDisplay = new Rect(offsetX, offsetY, 155f, displayHeight);
        offsetX += 155f;
        EditorGUI.LabelField(windowDisplay, "Rotate back to origin over");
        windowDisplay = new Rect(offsetX, offsetY, 50f, displayHeight);
        offsetX += 50f;
        rotationSpeed[rotationSpeed.Length - 1] = EditorGUI.FloatField(windowDisplay, rotationSpeed[rotationSpeed.Length - 1]);
        windowDisplay = new Rect(offsetX, offsetY, 50f, displayHeight);
        EditorGUI.LabelField(windowDisplay, "secs");

    }

    void OnLostFocus()
    {
        switch (chainType)
        {
            case ChainType.MOVEMENT:
                engine.movements[facingFocus].lockTimes = lockTimes;
                engine.movements[facingFocus].rotationSpeed = rotationSpeed;
                engine.movements[facingFocus].targets = targets;
                break;
            case ChainType.FACING:
                engine.facings[facingFocus].lockTimes = lockTimes;
                engine.facings[facingFocus].rotationSpeed = rotationSpeed;
                engine.facings[facingFocus].targets = targets;
                break;
        }
    }
}
