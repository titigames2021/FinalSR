using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Scroll : MonoBehaviour
{
    
    public float speed;
    public bool isBossStopped = false;
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
    
    private GameObject lvlcompletedPoint_obj;
    public bool towater;

    public GameObject movingSky;

    public GameObject submarine;
    public bool toboss;
    private bool bossdefeated;

    public GameObject placeholder;



    public Enemy1_5 submarine_script;
    public Enemy3_3 boat;
    public GameObject boos;
    public B03 boss3;
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


        }




       





    }

   
    //ESTO ESTA FATALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL
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
            case 3:
                

                if (!boos.active)
                {
                    speed = startSpeed;

                }


                if (gameObject.transform.position.x >= 497.0f && !isBossStopped)
                {

                    BossStop();
                    closeDoor = true;
                }


               
            //STAGE 2

                break;

            case 15:
          
                if (!boos.active)
                {
                    speed = startSpeed;

                }



                if (player.transform.position.x >= 654.0f && !isBossStopped)
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
            case 9:
            
               
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
                if (submarine_script.placeholderbool)
                {
                    
                    submarine_script.placeholderbool = false;

                    ToBoss();


                }

                if (boat.toWaterBoat)
                {
                    ToWater();
                }

                if (boss3.bossIsDead)
                {
                    ToFinal();
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
        
      
        if (!boos.activeSelf)
        {
            boos.SetActive(true);
        }
        
    }

    private void ToFinal()
    {
      




        lvlcompletedPoint.position = new Vector3(gameObject.transform.position.x + 10.0f, gameObject.transform.position.y, gameObject.transform.position.z);

        



    }


    private void OnDisable()
    {
       
        Destroy(gameObject);
    }
}
