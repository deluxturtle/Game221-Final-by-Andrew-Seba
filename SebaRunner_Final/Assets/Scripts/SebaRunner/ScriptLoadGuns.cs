﻿using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

public class ScriptLoadGuns : MonoBehaviour {

    [Header("Text Labels")]
    public GameObject nameText;
    public GameObject typeText;
    public GameObject damageText;
    public GameObject clipSizeText;
    public GameObject fireRateText;
    public GameObject automaticText;


    DirectoryInfo info;
    StreamReader reader;

    [HideInInspector]
    public List<ScriptGun> loadedGuns = new List<ScriptGun>();

    int gunIndex = 0;

    string internalPath = "/Resources/Weapons";
    string externalPath = "Weapons";

    public ScriptLevelSelection gameSetup;

    // Use this for initialization
    void Start () {
        string assetText;
        gameSetup = GameObject.Find("GameSetup").GetComponent<ScriptLevelSelection>();

        //#if UNITY_EDITOR

        //        info = new DirectoryInfo(Application.dataPath + internalPath);
        //        FileInfo[] guns = info.GetFiles();

        //        foreach (FileInfo file in guns)
        //        {
        //            if (file.Name.EndsWith(".txt"))
        //            {

        //                reader = file.OpenText();

        //                ScriptGun tempGun = new ScriptGun();

        //                string lineOfText;
        //                while ((lineOfText = reader.ReadLine()) != null)
        //                {
        //                    string variable = "";
        //                    string value = "";
        //                    List<string> words = new List<string>();
        //                    foreach (Match item in Regex.Matches(lineOfText, "\"(.*?)\""))
        //                    {
        //                        words.Add(item.ToString().ToUpper().TrimStart('"').TrimEnd('"'));
        //                    }
        //                    variable = words[0];
        //                    value = words[1];

        //                    #region Parse
        //                    if (variable != "" && value != "")
        //                    {
        //                        switch (variable)
        //                        {
        //                            case "GUNTYPE":
        //                                switch (value)
        //                                {
        //                                    case "PISTOL":
        //                                        tempGun.type = GunType.PISTOL;
        //                                        break;
        //                                    case "SMG":
        //                                        tempGun.type = GunType.SMG;
        //                                        break;
        //                                    case "RIFLE":
        //                                        tempGun.type = GunType.RIFLE;
        //                                        break;
        //                                    case "SNIPER":
        //                                        tempGun.type = GunType.SNIPER;
        //                                        break;
        //                                    case "SPECIAL":
        //                                        tempGun.type = GunType.SPECIAL;
        //                                        break;
        //                                }
        //                                break;
        //                            case "DAMAGE":
        //                                try
        //                                {
        //                                    tempGun.damage = float.Parse(value);
        //                                }
        //                                catch
        //                                {
        //                                    throw new UnityException("Damage value wasn't able to convert to float.");
        //                                }
        //                                break;
        //                            case "CLIPSIZE":
        //                                try
        //                                {
        //                                    tempGun.clipSize = int.Parse(value);
        //                                }
        //                                catch
        //                                {
        //                                    throw new UnityException("Clip size wasn't able to convert to int.");
        //                                }
        //                                break;
        //                            case "FIRERATE":
        //                                try
        //                                {
        //                                    tempGun.fireRate = float.Parse(value);
        //                                }
        //                                catch
        //                                {
        //                                    throw new UnityException("Fire rate value wasn't able to convert to float.");
        //                                }
        //                                break;
        //                            case "AUTOMATIC":
        //                                if (value == "TRUE")
        //                                    tempGun.automatic = true;
        //                                else
        //                                    tempGun.automatic = false;
        //                                break;
        //                            case "GUNNAME":
        //                                tempGun.gunName = UppercaseFirst(value.ToLower());

        //                                break;
        //                            default:
        //                                Debug.Log(variable + ": Defaulted");
        //                                break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Debug.Log("either the reader broke or invalid string detected.");
        //                    }
        //                    #endregion

        //                }
        //                loadedGuns.Add(tempGun);
        //            }
        //        }

        //#else

        //#endif

        object[] embeddedGuns = Resources.LoadAll("Weapons");
        if (embeddedGuns != null)
        {
            foreach (TextAsset file in embeddedGuns)
            {
                string allText = file.text;
                StringReader stringReader = new StringReader(allText);

                ScriptGun tempGun = new ScriptGun();

                string lineOfText;
                while ((lineOfText = stringReader.ReadLine()) != null)
                {
                    string variable = "";
                    string value = "";
                    List<string> words = new List<string>();
                    foreach (Match item in Regex.Matches(lineOfText, "\"(.*?)\""))
                    {
                        words.Add(item.ToString().ToUpper().TrimStart('"').TrimEnd('"'));
                    }
                    variable = words[0];
                    value = words[1];

                    #region Parse
                    if (variable != "" && value != "")
                    {
                        switch (variable)
                        {
                            case "GUNTYPE":
                                switch (value)
                                {
                                    case "PISTOL":
                                        tempGun.type = GunType.PISTOL;
                                        break;
                                    case "SMG":
                                        tempGun.type = GunType.SMG;
                                        break;
                                    case "RIFLE":
                                        tempGun.type = GunType.RIFLE;
                                        break;
                                    case "SNIPER":
                                        tempGun.type = GunType.SNIPER;
                                        break;
                                    case "SPECIAL":
                                        tempGun.type = GunType.SPECIAL;
                                        break;
                                }
                                break;
                            case "DAMAGE":
                                try
                                {
                                    tempGun.damage = float.Parse(value);
                                }
                                catch
                                {
                                    throw new UnityException("Damage value wasn't able to convert to float.");
                                }
                                break;
                            case "CLIPSIZE":
                                try
                                {
                                    tempGun.clipSize = int.Parse(value);
                                }
                                catch
                                {
                                    throw new UnityException("Clip size wasn't able to convert to int.");
                                }
                                break;
                            case "FIRERATE":
                                try
                                {
                                    tempGun.fireRate = float.Parse(value);
                                }
                                catch
                                {
                                    throw new UnityException("Fire rate value wasn't able to convert to float.");
                                }
                                break;
                            case "AUTOMATIC":
                                if (value == "TRUE")
                                    tempGun.automatic = true;
                                else
                                    tempGun.automatic = false;
                                break;
                            case "GUNNAME":
                                tempGun.gunName = UppercaseFirst(value.ToLower());

                                break;
                            default:
                                Debug.Log(variable + ": Defaulted");
                                break;
                        }
                    }
                    else
                    {
                        Debug.Log("either the reader broke or invalid string detected.");
                    }
                    #endregion

                }
                loadedGuns.Add(tempGun);
            }
        }
        if (loadedGuns.Count > 0)
            LoadGun(loadedGuns[0]);
        else
        {
#if UNITY_EDITOR
            Debug.Log("No Guns Found in " + internalPath);
#else
            Debug.Log("No Guns Found in " + externalPath);
#endif
        }

        gameSetup.SetMyGun(loadedGuns[gunIndex]);//Send the first gun to setup
    }

    //Used this to uppercase the name.
    string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        return char.ToUpper(s[0]) + s.Substring(1);
    }

    void LoadGun(ScriptGun gun)
    {
        nameText.GetComponent<Text>().text = gun.gunName;
        typeText.GetComponent<Text>().text = gun.type.ToString();
        damageText.GetComponent<Text>().text = gun.damage.ToString();
        clipSizeText.GetComponent<Text>().text = gun.clipSize.ToString();
        fireRateText.GetComponent<Text>().text = gun.fireRate.ToString();
        automaticText.GetComponent<Text>().text = gun.automatic.ToString();
    }

    public void LoadNextGun()
    {
        if(loadedGuns.Count > 0)
        {
            gunIndex = (gunIndex + 1) % loadedGuns.Count;
            LoadGun(loadedGuns[gunIndex]);
        }
        gameSetup.SetMyGun(loadedGuns[gunIndex]);
    }

    public void LoadPreviousGun()
    {
        if(loadedGuns.Count > 0)
        {
            gunIndex -= 1;
            if (gunIndex < 0)
            {
                gunIndex = loadedGuns.Count - 1;
                LoadGun(loadedGuns[gunIndex]);
            }
            else
            {
                LoadGun(loadedGuns[gunIndex]);
            }
            gameSetup.SetMyGun(loadedGuns[gunIndex]);
        }
    }


}
