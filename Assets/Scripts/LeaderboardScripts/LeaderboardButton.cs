using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject LeaderBoard;
    [SerializeField] PlayerScoreList playerScoreList;


    public void OpenLeaderboard()
    {
        mainMenu.gameObject.SetActive(false);
        LeaderBoard.SetActive(true);
        playerScoreList.UpdateLeaderboard();
    }

    public void CloseLeaderboard()
    {
        mainMenu.gameObject.SetActive(true);
        LeaderBoard.SetActive(false);
    }

    public void SortByTime()
    {
        playerScoreList.UpdateLeaderboardSortedByTime();
    }
    public void SortByLevel()
    {
        playerScoreList.UpdateLeaderboardSortedByLevel();
    }
}
