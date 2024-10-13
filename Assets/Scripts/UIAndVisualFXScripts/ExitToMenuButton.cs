using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenuButton : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerData playerData;
    [SerializeField] JsonReader jsonReader;

    public void SaveAndExitToMainMenu()
    {
        jsonReader.SaveData(playerData.playerName, playerData.timePlaying, playerData.Level);
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitToMainMenuWithoutSaving()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClosePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
        GamesManager.Instance.SwitchState<PlayingState>();
    }
}
