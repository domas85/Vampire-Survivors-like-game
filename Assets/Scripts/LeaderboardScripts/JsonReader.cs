using UnityEngine;
using System;
using System.Linq;
using System.IO;
using Unity.VisualScripting;
using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;

public class JsonReader : MonoBehaviour
{
    private ScoreData scoreData = new();
    private string path = "";
    private string persistentPath = "";

    private void Awake()
    {
        SetPaths();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SavedScores.json"; //path for easy testing file path the assets folder
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SavedScores.json"; //set to this path when building the game it saves to %appdata%
    }


    public void SaveData(string playerName, float playTime, int playerLevel)
    {
        string savepath = persistentPath;
        Debug.Log("Saving Score at " + savepath);

        scoreData = LoadData();
        scoreData.scoreEntries.Add(new ScoreEntry(playerName, playTime, playerLevel));
        string json = JsonUtility.ToJson(scoreData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savepath);
        writer.Write(json);
    }

    public void CreateFile()
    {
        string savepath = persistentPath;
        Debug.Log("Creating new Score data file at " + savepath);
        string json = JsonUtility.ToJson(scoreData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savepath);
        writer.Write(json);
    }

    public ScoreData LoadData()
    {
        if (!File.Exists(persistentPath))
        {
            CreateFile();
        }
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        ScoreData data = JsonUtility.FromJson<ScoreData>(json);
        Debug.Log("data was loaded");
        return data;
    }

}

