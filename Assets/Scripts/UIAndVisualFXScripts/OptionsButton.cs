using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    public void OpenOptionsMenu()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        mainMenu.gameObject.SetActive(true);
        optionsMenu.SetActive(false);
    }

}
