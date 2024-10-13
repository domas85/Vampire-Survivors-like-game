using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameSelectionButton : MonoBehaviour
{
    [SerializeField] GameObject selectNameScreen;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] PlayerData playerData;

    public void CloseInputMenu()
    {
        selectNameScreen.SetActive(false);
        warningText.text = "";
        inputField.text = "";
    }

    public void ConfirmAndEnterGame()
    {
        if (inputField.text.Length <= 2)
        {
            warningText.text = "Please enter a Name of at least 3 characters";
        }
        else if (inputField.text.Length >= 15)
        {
            warningText.text = "Entered Name is too long, your name should be the maximum of 15 characters";
        }
        else
        {
            playerData.playerName = inputField.text;
            SceneManager.LoadScene("Gameplay");
        }
    }
}
