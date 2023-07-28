using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMenu : MonoBehaviour
{
    public GameObject continueUI;

    private void Update()
    {
        if (GameManager.Instance.savedGame)
        {
            continueUI.SetActive(true);
        }
        else
        {
            continueUI.SetActive(false);
        }
    }


    public void StartLevelMainMenu(int level)
    {
        GameManager.Instance.StartLevel(level);
    }

    public void NewGameMenu()
    {
        GameManager.Instance.NewGame();
    }

    public void ExitGameMenu()
    {
        GameManager.Instance.ExitGame();
    }





}
