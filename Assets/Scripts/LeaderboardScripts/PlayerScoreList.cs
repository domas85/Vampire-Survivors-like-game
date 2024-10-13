using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScoreList : MonoBehaviour
{
    public GameObject playerScoreEntryPrefab;

    [SerializeField] ScoreManager scoreManager;

    public void UpdateLeaderboard()
    {
        if (scoreManager == null)
        {
            Debug.LogWarning("No score manager component found");
            return;
        }

        while (this.transform.childCount > 0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);

            Destroy(c.gameObject);
        }


        //string[] sortedNames = scoreManager.GetPlayerNames("time");

        ScoreEntry[] enties = scoreManager.GetScores();


        foreach (ScoreEntry entry in enties)
        {
            var myTimeSpan = TimeSpan.FromSeconds(entry.time);

            var formatedTime = $"{(int)myTimeSpan.TotalMinutes}:{myTimeSpan.Seconds:00}";

            GameObject go = Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.name;
            go.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = formatedTime;
            go.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = entry.level.ToString();

            //go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
            //go.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = scoreManager.GetScore(name, "time").ToString();
            //go.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = scoreManager.GetScore(name, "level").ToString();
        }

        //foreach (string name in sortedNames)
        //{
        //    GameObject go = Instantiate(playerScoreEntryPrefab);
        //    go.transform.SetParent(this.transform);
        //    go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
        //    go.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = scoreManager.GetScore(name, "time").ToString();
        //    go.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = scoreManager.GetScore(name, "level").ToString();


        //    //go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
        //    //go.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = scoreManager.GetScore(name, "time").ToString();
        //    //go.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = scoreManager.GetScore(name, "level").ToString();
        //}
    }

    public void UpdateLeaderboardSortedByTime()
    {
        if (scoreManager == null)
        {
            Debug.LogWarning("No score manager component found");
            return;
        }

        while (this.transform.childCount > 0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);

            Destroy(c.gameObject);
        }

        var entiesSortedByTime = scoreManager.GetSortedScoresByTime();



        foreach (ScoreEntry entry in entiesSortedByTime)
        {
            var myTimeSpan = TimeSpan.FromSeconds(entry.time);

            var formatedTime = $"{(int)myTimeSpan.TotalMinutes}:{myTimeSpan.Seconds:00}";

            GameObject go = Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.name;
            go.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = formatedTime;
            go.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = entry.level.ToString();

        }
    }

    public void UpdateLeaderboardSortedByLevel()
    {
        if (scoreManager == null)
        {
            Debug.LogWarning("No score manager component found");
            return;
        }

        while (this.transform.childCount > 0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);

            Destroy(c.gameObject);
        }

        var entiesSortedByLevel = scoreManager.GetSortedScoresByLevel();

        foreach (ScoreEntry entry in entiesSortedByLevel)
        {
            var myTimeSpan = TimeSpan.FromSeconds(entry.time);

            var formatedTime = $"{(int)myTimeSpan.TotalMinutes}:{myTimeSpan.Seconds:00}";

            GameObject go = Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.name;
            go.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = formatedTime;
            go.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = entry.level.ToString();
        }
    }
}
