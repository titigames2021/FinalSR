using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scroll : MonoBehaviour
{
    public float speed;
    private bool isBossStopped = false;
    public float stopPoint;
    public GameObject player;
   
    public bool boosDefeated;
    private float startSpeed;
    public Transform boosDoor;

    public PlayerController _player;
    private bool closeDoor;
    //LEVEL3
    //Puntos donde tiene que bajar de altura
    public Transform dp1;
    public Transform dp2;
    public Transform hp1;
    public Transform hp2;
    public Transform hp3;
    public bool dp1finish;
    

   
    public Transform lvlcompletedPoint;
    public int currentLevel;
    public GameObject boos;
    private GameObject lvlcompletedPoint_obj;
    public bool towater;

    public GameObject movingSky;

    public GameObject submarine;
    public bool toboss;
    private bool bossdefeated;



    // Start is called before the first frame update
    void Start()
    {


      

        lvlcompletedPoint_obj = GameObject.FindWithTag("levelcompleted");
        lvlcompletedPoint = lvlcompletedPoint_obj.gameObject.GetComponent<Transform>();
        startSpeed = speed;
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


            case 4:
                currentLevel = 4;

                break;

            case 5:
                currentLevel = 5;

                break;

            case 6:
                currentLevel = 6;

                break;


            case 7:
                currentLevel = 7;

                break;

            case 8:
                currentLevel = 8;

                break;


        }




        //EVENTS 
        if(currentLevel==4)
        {
            Enemy3_3 boat = FindObjectOfType<Enemy3_3>();
            boat.waterEvent += ToWater;

            Enemy1_5 submarine_script = FindObjectOfType<Enemy1_5>();
            submarine_script.bossEvent += ToBoss;

            B03 bosscript = FindObjectOfType<B03>();
            bosscript.bossDefeatEvent += ToFinal;

        }
        




    }

 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);


        if (player.transform.position.x >= lvlcompletedPoint.position.x)
        {

            GameManager.Instance.LevelCompleted();



        }






        if (_player.life <= 0)
        {
            speed = 0;
        }
        switch (currentLevel)
        {
            //STAGE 1
            case 2:
            case 5:

                if (!boos.active)
                {
                    speed = startSpeed;

                }


                if (gameObject.transform.position.x >= 490.0f && !isBossStopped)
                {

                    BossStop();
                    closeDoor = true;
                }


               
            //STAGE 2

                break;

            case 3:
            case 6:
                if (!boos.active)
                {
                    speed = startSpeed;

                }



                if (player.transform.position.x >= 650.0f && !isBossStopped)
                {

                    BossStop();
                    closeDoor = true;
                }

                if (closeDoor)
                {
                    CloseDoor();

                }


                break;

            //STAGE 3
            case 4:
            case 7:
                if (transform.position.x >= dp1.position.x && transform.position.y >= hp2.position.y)
                {
                    
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                    dp1finish = true;
                }


                if (towater && transform.position.y >= hp3.position.y && dp1finish)
                {
                    
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                }

                if (transform.position.x >= movingSky.transform.position.x && transform.position.y >= hp3.position.y && dp1finish)
                {
                   
                    Vector3 skyPos = movingSky.transform.position;
                    skyPos.x = player.transform.position.x + 20.0f;
                    movingSky.transform.position = skyPos;
                }


                break;


        }




        



       

     




    }

    private void CloseDoor()
    {

        if (boosDoor.position.y >= -2.0f)
        {
            boosDoor.Translate(Vector2.down * Time.deltaTime);
        }
   

    }

    private void BossStop()
    {
        speed -= Time.deltaTime;
        if (speed <= 0 )
        {
            isBossStopped = true;
            
            
        }

       
        

    }



    //Events 

    private void ToWater()
    {
        towater = true;
        if (!submarine.activeSelf)
        {
            submarine.SetActive(true);
        }
      
    }



    private void ToBoss()
    {
        toboss = true;
        if (!boos.activeSelf)
        {
            boos.SetActive(true);
        }

    }

    private void ToFinal()
    {
        bossdefeated = true;




        lvlcompletedPoint.position = new Vector3(gameObject.transform.position.x + 10.0f, gameObject.transform.position.y, gameObject.transform.position.z);

        



    }
}
