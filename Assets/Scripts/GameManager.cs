using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerPrefs;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public int currentLevel;
    public bool level1completed;
    public bool level2completed;
    public bool level3completed;
    public bool isPaused;
   

    public bool savedGame;

    

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level1Completed") || PlayerPrefs.HasKey("Level2Completed") || PlayerPrefs.HasKey("Level3Completed"))
        {
            savedGame = true;
        }



        // Cargar los valores de los bools desde PlayerPrefs
        level1completed = PlayerPrefs.GetInt("Level1Completed") == 1;
        level2completed = PlayerPrefs.GetInt("Level2Completed") == 1;
        level3completed = PlayerPrefs.GetInt("Level3Completed") == 1;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;




        }
        instance = this;
        DontDestroyOnLoad(gameObject);
       

    }





    public void LevelCompleted()
    {
       
               
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
                level1completed = true;
                
                PlayerPrefs.SetInt("Level1Completed", level1completed ? 1 : 0); // Guardar el valor bool como entero
                savedGame = true;
                SceneManager.LoadScene(1);
                Cursor.visible = true;
                break;


            case 3:
                level2completed = true;
                
                PlayerPrefs.SetInt("Level2Completed", level2completed ? 1 : 0); // Guardar el valor bool como entero
                savedGame = true;
                SceneManager.LoadScene(1);
                Cursor.visible = true;
                break;

            case 4:
                level3completed = true;
                PlayerPrefs.SetInt("Level3Completed", level3completed ? 1 : 0); // Guardar el valor bool como entero
                savedGame = true;
                SceneManager.LoadScene(1);
                Cursor.visible = true;

                break;

            case 5:
               

               
                SceneManager.LoadScene(8);
                Cursor.visible = true;
                break;


            case 6:
               

                
                SceneManager.LoadScene(8);
                Cursor.visible = true;

                break;

            case 7:
               
                SceneManager.LoadScene(8);
                Cursor.visible = true;

                break;


        }


    }


    public int CurrentLevel()
    {
        return currentLevel;
    }




    public void StartLevel( int level)
    {

        SceneManager.LoadScene(level);
        if (level != 0)
        {
            Cursor.visible = false;
        }
       
        currentLevel = level;
        if (isPaused)
        {
            ResumeGame();
        }


    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
       

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
       
    }


    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {

       
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        level1completed = PlayerPrefs.GetInt("Level1Completed") == 1;
        level2completed = PlayerPrefs.GetInt("Level2Completed") == 1;
        level3completed = PlayerPrefs.GetInt("Level3Completed") == 1;
        savedGame = false;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
