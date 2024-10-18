using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] JsonReader reader;

    //  Dictionary<string, Dictionary<string, int>> playerScoress;

    ScoreData scoreData;
    List<ScoreEntry> sortedScoreData;

    int clickToggle;

    void Init()
    {
        scoreData = reader.LoadData();


        //if (playerScoress != null) { return; }

        //playerScoress = new Dictionary<string, Dictionary<string, int>>();
    }

    public ScoreEntry GetScore(string playerName)
    {
        Init();
        scoreData = reader.LoadData();

        //get multiple entries with the same name
        ScoreEntry[] nameEntries = scoreData.scoreEntries.FindAll(x => x.name == playerName).ToArray();

        //get one entry with the inputed name
        ScoreEntry singleEntry = scoreData.scoreEntries.Find(x => x.name == playerName);

        return singleEntry;

        ////No score for this player with this Name
        //if (playerScoress.ContainsKey(playerName) == false) { return 0;}

        //if (playerScoress[playerName].ContainsKey(scoreType) == false) {  return 0; }

        //return playerScoress[playerName][scoreType];
    }

    public ScoreEntry[] GetScores()
    {
        Init();
        return scoreData.scoreEntries.ToArray();
    }

    public List<ScoreEntry> GetSortedScoresByTime()
    {
        Init();

        switch (clickToggle)
        {
            case 0:
                sortedScoreData = scoreData.scoreEntries.OrderByDescending(entry => entry.time).ToList();
                clickToggle = 1;
                break;
            case 1:
                sortedScoreData = scoreData.scoreEntries.OrderBy(entry => entry.time).ToList();
                clickToggle = 0;
                break;
            default:
                break;
        }

        return sortedScoreData;
    }

    public List<ScoreEntry> GetSortedScoresByLevel()
    {
        Init();

        switch (clickToggle)
        {
            case 0:
                sortedScoreData = scoreData.scoreEntries.OrderByDescending(entry => entry.level).ToList();
                clickToggle = 1;
                break;
            case 1:
                sortedScoreData = scoreData.scoreEntries.OrderBy(entry => entry.level).ToList();
                clickToggle = 0;
                break;
            default:
                break;
        }

        return sortedScoreData;
    }

    //public void SetScore(string playerName, string scoreType, int value)
    //{
    //    Init();

    //    if (playerScoress.ContainsKey(playerName) == false)
    //    {
    //        playerScoress[playerName] = new Dictionary<string, int>();
    //    }

    //    playerScoress[playerName][scoreType] = value;

    //}

    //public void ChangeScore(string playerName, string scoreType, int amount)
    //{
    //    Init();
    //    int currentScore = 0;// GetScore(playerName, scoreType)[0].level;
    //    //int currentTime = GetScore(playerName, scoreType)[3].time;
    //    SetScore(playerName, scoreType, currentScore + amount);
    //}

    //public string[] GetSortedScores(string sortingScoreType)
    //{
    //    Init();

    //    return playerScoress.Keys.OrderByDescending(n => GetScore(n, sortingScoreType)).ToArray();
    //}
}
