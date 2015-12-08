using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ScriptGunCreatorWindow : EditorWindow {

    GunType gunType = GunType.PISTOL;
    string gunName = "Gun";
    float damage = 5.0f;
    int clipSize = 17;
    float fireRate = 0.02f;
    bool automatic = false;

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New"))
        {
            if(EditorUtility.DisplayDialog("Start a new gun?",
                "You will lose any unsaved changes.", "Ok", "Cancel"))
            {
                gunType = GunType.PISTOL;
                gunName = "Gun";
                damage = 5.0f;
                clipSize = 17;
                fireRate = 0.02f;
                automatic = false;

                this.Repaint();
            }

        }
        //if (GUILayout.Button("Load"))
        //{
        //    var path = EditorUtility.OpenFilePanel("Open file", Application.dataPath + "/Weapons/", ".txt");

        //}
        GUILayout.EndHorizontal();

        GUILayout.Label("Gun Settings", EditorStyles.boldLabel);
        gunName = EditorGUILayout.TextField("Gun Name", gunName);
        gunType = (GunType)EditorGUILayout.EnumPopup("Gun Type", gunType);
        damage = EditorGUILayout.FloatField("Damage", damage);
        clipSize = EditorGUILayout.IntField("Clip Size", clipSize);
        fireRate = EditorGUILayout.FloatField("Fire Rate", fireRate);
        automatic = EditorGUILayout.Toggle("Automatic", automatic);

        if(GUILayout.Button("Save Gun"))
        {
            SaveGun();
            this.Close();
        }
    }

    void SaveGun()
    {
        ScriptGun tempGun = new ScriptGun(gunName, gunType, damage, clipSize, fireRate, automatic);
        if(!Directory.Exists(Application.dataPath + "/Weapons"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Weapons");
        }

        System.IO.File.WriteAllText(Application.dataPath + "/Weapons/" + gunName + ".txt",
            "\"GunName\" \"" + tempGun.gunName + "\"\n" +
            "\"GunType\" \"" + tempGun.type + "\"" + "\n" +
            "\"Damage\" \"" + tempGun.damage + "\"" + "\n" +
            "\"ClipSize\" \"" + tempGun.clipSize + "\"" + "\n" +
            "\"FireRate\" \"" + tempGun.fireRate + "\"" + "\n" +
            "\"Automatic\" \"" + tempGun.automatic.ToString() + "\""
            );

        AssetDatabase.Refresh();
        Debug.Log("Created \"/Weapons/" + gunName + ".txt\"");
    }
}
