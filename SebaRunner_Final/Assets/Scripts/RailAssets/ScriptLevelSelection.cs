using UnityEngine;


public class ScriptLevelSelection : MonoBehaviour {

    public string selectedLevel;
    //[HideInInspector]
    public ScriptGun myGun = new ScriptGun();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void _SetLevelToLevel1()
    {
        selectedLevel = "shooterTest";

    }

    public void SetMyGun(ScriptGun pGun)
    {
        ScriptGun tempGun = new ScriptGun(
            pGun.gunName,
            pGun.type,
            pGun.damage,
            pGun.clipSize,
            pGun.fireRate,
            pGun.automatic);
        myGun = tempGun;
    }

    public ScriptGun GetGun()
    {
        return myGun;
    }
}
