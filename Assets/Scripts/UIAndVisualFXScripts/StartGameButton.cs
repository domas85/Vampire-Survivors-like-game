using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] GameObject selectNameScreen;
    public void GoToEnterName()
    {
        selectNameScreen.gameObject.SetActive(true);
    }
}
