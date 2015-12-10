using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Author: Andrew Seba
/// </summary>
public class ScriptLoad : MonoBehaviour {

	public Text textConsole;

	TextAsset textFile;
	StreamReader reader;

	DirectoryInfo info;

	//for testing to see if levels are there
	List<string> levelNames = new List<string>();

	//read in the levels into this
	public List<Item> levels = new List<Item>();

	void Start()
	{
		//read in files in a directory
		info = new DirectoryInfo(Application.dataPath + "/Resources");
		FileInfo[] levelInfo = info.GetFiles();

		foreach(FileInfo file in levelInfo)
		{
			if (file.Name.EndsWith(".srl"))
			{
				levelNames.Add(file.Name);
				textConsole.text += "\n" + file;

				reader = file.OpenText();


				string lineOfText;
				//int lineNumber = 0;
				Item tempItem = new Item();
                if(reader != null)
                {
                    while ((lineOfText = reader.ReadLine()) != null)
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
                        //lineNumber++;

                        if (variable != "" && value != "")
                        {
                            switch (variable)
                            {
                                case "AUTHOR":
                                    tempItem.author = value;
                                    break;
                                case "DATE":
                                    tempItem.dateCreated = value;
                                    break;
                                case "LVLNAME":
                                    tempItem.name = value;
                                    break;
                            }
                        }
                        //Leave If we got all the basic information read in.
                        if (tempItem.author != null && tempItem.dateCreated != null && tempItem.name != null)
                        {
                            break;
                        }
                    }
                    tempItem.fileName = file.Name;

                    levels.Add(tempItem);
                    reader.Close();
                }
            }
		}

		if(levelNames.Count <= 0 && textConsole != null)
		{
			textConsole.text += "\nNo Levels Found in <" + Application.dataPath + "/>"; 
		}

		//BroadcastMessage("LoadInLevelList");
		ScriptCreateScrollList scrollList = 
			gameObject.GetComponent<ScriptCreateScrollList>();
		scrollList.LoadInLevelList();

	}


	public void ImportLevel(string pLevelName)
	{

        FileInfo levelReadingData = new FileInfo(Application.dataPath + "/Resources/" + FindLevel(pLevelName));
        reader = levelReadingData.OpenText();

        string input = reader.ReadToEnd();
        reader.Close();

        File.WriteAllText(Application.dataPath + "/waypoints.txt", input);
    }

    string FindLevel(string pName)
	{
		foreach(Item levelItem in levels)
		{
            if (pName == levelItem.name)
			{

				return levelItem.fileName;
			}
		}
		return "/Resources/embedded.txt";
	}
}
