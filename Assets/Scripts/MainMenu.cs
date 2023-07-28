using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool lvl1;
    private bool lvl2;
    private bool lvl3;


    public Button lvl1_btn;
    public Button lvl2_btn;
    public Button lvl3_btn;
    public int currentLevel;
    public Color darkGreen;

    // Start is called before the first frame update
    void Start()
    {
        darkGreen = new Color(0f, 0.5f, 0f);
        if (GameManager.Instance.level1completed)
        {
            lvl1 = true;
        }


        if (GameManager.Instance.level2completed)
        {
            lvl2 = true;
        }


        if (GameManager.Instance.level3completed)
        {
            lvl3 = true;
        }

       


        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                currentLevel = 0;

                break;


            case 1:
                currentLevel = 1;

                break;

            case 2:
                currentLevel = 2;

                break;

            case 3:
                currentLevel = 3;

                break;


        }


    }

    // Update is called once per frame
    void Update()
    {


        if (lvl1)
        {
           
            ColorBlock colorBlock = lvl1_btn.colors;
            colorBlock.normalColor = darkGreen;
           
           
            colorBlock.highlightedColor = Color.green;
         

            colorBlock.selectedColor = Color.green;
            lvl1_btn.colors = colorBlock;
        }

        if (lvl2)
        {
            ColorBlock colorBlock = lvl2_btn.colors;
            colorBlock.normalColor = darkGreen;


            colorBlock.highlightedColor = Color.green;


            colorBlock.selectedColor = Color.green;
            lvl2_btn.colors = colorBlock;
        }

        if (lvl3)
        {
            ColorBlock colorBlock = lvl3_btn.colors;
            colorBlock.normalColor = darkGreen;


            colorBlock.highlightedColor = Color.green;


            colorBlock.selectedColor = Color.green;
            lvl3_btn.colors = colorBlock;
        }






    }


    public void StartLevelMainMenu(int level)
    {
        GameManager.Instance.StartLevel(level);
    }

  

}
