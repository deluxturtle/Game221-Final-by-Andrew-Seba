using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ScriptEditorOrganizer : MonoBehaviour {
    
    
    [MenuItem("Tool Creation/Create Project Folders")]
    public static void Createfolder()
    {
        //Create All the project folders.
        //Assets/Dynamic Assets
        AssetDatabase.CreateFolder("Assets", "Dynamic Assets");

        //Assets/Dynamic Assets/Resources
        AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Resources");

        //Assets/Dynamic Assets/Resources/Animations
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Animations");
        //Assets/Dynamic Assets/Resources/Animations/Sources
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Animations", "Sources");

        //Assets/Dynamic Assets/Resources/Animation Controllers
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Animation Controllers");

        //Assets/Dynamic Assets/Resources/Effects
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Effects");
        
        //Assets/Dynamic Assets/Resources/Models
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Models");
        //Assets/Dynamic Assets/Resources/Models/Characters
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Models", "Characters");
        //Assets/Dynamic Assets/Resources/Models/Environment
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Models", "Environment");

        //Assets/Dynamic Assets/Resources/Prefabs
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Prefabs");
        //Assets/Dynamic Assets/Resources/Prefabs/Common
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Prefabs", "Common");

        //Assets/Dynamic Assets/Resources/Sounds
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Sounds");
        //Assets/Dynamic Assets/Resources/Sounds/Music
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds", "Music");
        //Assets/Dynamic Assets/Resources/Sounds/Music/Common
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds/Music", "Common");
        //Assets/Dynamic Assets/Resources/Sounds/SFX
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds", "SFX");
        //Assets/Dynamic Assets/Resources/Sounds/SFX/Common
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds/SFX", "Common");
        //Assets/Dynamic Assets/Resources/Sounds
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Textures");
        //Assets/Dynamic Assets/Resources/folderStructure.txt
        System.IO.File.WriteAllText(Application.dataPath + "/Dynamic Assets/Resources/folderStructure.txt", "Place all your dynamic resources in this folder.");

        //Assets/Editor
        AssetDatabase.CreateFolder("Assets", "Editor");
        //Assets/Editor/folderSctructure.txt
        System.IO.File.WriteAllText(Application.dataPath + "/Editor/folderStructure.txt", "Place editor scripts in this folder.");

        //Assets/Extensions
        AssetDatabase.CreateFolder("Assets", "Extensions");
        //Assets/Extensions/folderStructure.txt
        System.IO.File.WriteAllText(Application.dataPath + "/Extensions/folderStructure.txt", "Place extensions in this folder.");

        //Assets/Gizmos
        AssetDatabase.CreateFolder("Assets", "Gizmos");
        //Assets/Gizmos/folderStructure.txt
        System.IO.File.WriteAllText(Application.dataPath + "/Gizmos/folderStructure.txt", "Place Gizmos in this folder.");

        //Assets/Plugins
        AssetDatabase.CreateFolder("Assets", "Plugins");
        //Assets/Plugins/folderStructure.txt
        System.IO.File.WriteAllText(Application.dataPath + "/Plugins/folderStructure.txt", "Place plugins in this folder.");

        //Assets/Scripts
        AssetDatabase.CreateFolder("Assets", "Scripts");
        //Assets/Scripts/folderStructure.txt
        System.IO.File.WriteAllText(Application.dataPath + "/Scripts/folderStructure.txt", "Place Scripts in this folder.");
        //Assets/Scripts/Common
        AssetDatabase.CreateFolder("Assets/Scripts", "Common");

        //Assets/Shaders
        AssetDatabase.CreateFolder("Assets", "Shaders");
        System.IO.File.WriteAllText(Application.dataPath + "/Shaders/folderStructure.txt", "Place Shaders in this folder.");

        //Assets/Static Assets
        AssetDatabase.CreateFolder("Assets", "Static Assets");
        System.IO.File.WriteAllText(Application.dataPath + "/Static Assets/folderStructure.txt", "Place all static assets in this folder");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Animations");
        AssetDatabase.CreateFolder("Assets/Static Assets/Animations", "Sources");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Animation Controllers");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Effects");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Models");
        AssetDatabase.CreateFolder("Assets/Static Assets/Models", "Character");
        AssetDatabase.CreateFolder("Assets/Static Assets/Models", "Environment");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Prefabs");
        AssetDatabase.CreateFolder("Assets/Static Assets/Prefabs", "Common");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Scenes");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Sounds");
        AssetDatabase.CreateFolder("Assets/Static Assets/Sounds", "Music");
        AssetDatabase.CreateFolder("Assets/Static Assets/Sounds/Music", "Common");
        AssetDatabase.CreateFolder("Assets/Static Assets/Sounds", "SFX");
        AssetDatabase.CreateFolder("Assets/Static Assets/Sounds/SFX", "Common");
        AssetDatabase.CreateFolder("Assets/Static Assets", "Textures");
        AssetDatabase.CreateFolder("Assets/Static Assets/Textures", "Common");
        AssetDatabase.CreateFolder("Assets", "Testing");
        System.IO.File.WriteAllText(Application.dataPath + "/Testing/folderStructure.txt", "Place all testing objects or scenes in this folder");

        System.IO.File.WriteAllText(Application.dataPath + "/folderStructure.txt", "Please place assets in the corrisponding folders or in this folder if N/A");
        AssetDatabase.Refresh();
    }


}
