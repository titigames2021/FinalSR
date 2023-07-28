using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B01 : EnemyBase
{
    //States 
    public MindStates current_mind_state_;
    public enum MindStates
    {
        kWait,
        kAttack01,
        kAttack02,
        kAttack03
    }

    private Vector3 target;
    public float shoot_distance = 0.0f;
    public bool stop;
    public Vector3 distancePlayer;
    private float secondwait = 5.0f;
    private float countDowndSecondWait;
    public float limitUp;
    public bool goingUp;
    public bool goingDown;
    public float limitDown;

    public GameObject weakSpot1;
    public GameObject weakSpot2;
    public GameObject weakSpot3;

    private int lifeWS1=8;
    private int lifeWS2=8;
   public int lifeWS3=8;


   
    public bool shootThunder;
    private bool lookingRight;
    private bool lookingUP;
    private bool lookingLeft;
    
    public bool canMove;
    private float stopUP = -0.7071068f;
    private float stopLeft = 0.0f;
    private float stopRight = 0.9999908f;
    private float rotationSpeed = 20.0f;
    public GameObject thunderTrigger;
    private bool thunderTriggerInstantiated;

    public ArrayList thunders;
    public Animator thunderAnimator;
    public Animator beamAnimator;
   
    private float thunderTime;


    public float timeToChange = 8.0f;
    public bool a3BOOL;
    public bool a2BOOL;
    public bool a1BOOL;

    private float beamtime;
    private bool slowTime;
    private float startSpeed;
    float t2 = 0.0f;

    float t = 0.0f;

    private float timeA1;
    private float timeA2;
    private float timeA3;
    private float minimumTimeAttack = 8.0f;
    private bool minimumTimeA1Finish;
    private bool minimumTimeA2Finish;




    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();

        canShoot = true;

        startSpeed = speed;
        lookingLeft = true;

    }

    // Update is called once per frame
    void Update()
    {
       

        //Debug.Log(gameObject.transform.rotation.z);
        //Life
        CheckLife();

        //Activation
        //DisableEnemy();
        DistacePlayer();
        ActiveEnemy();

        //Mindstates
        if (current_mind_state_ == MindStates.kWait)
        {
            MindWait();
        }
        else if (current_mind_state_ == MindStates.kAttack01)
        {
            MindAttack01();
        }
        else if (current_mind_state_ == MindStates.kAttack02)
        {
            MindAttack02();
        }
        else if (current_mind_state_ == MindStates.kAttack03)
        {
            MindAttack03();
        }





        //Debug.Log(current_mind_state_);





        //DAMAGE 

        if (lifeWS1 <= 0 && lifeWS2 <= 0 && lifeWS3 <= 0)
        {

            gameObject.SetActive(false);
        }

        if(lifeWS1 <= 0)
        {
            weakSpot1.SetActive(false);
        }

        if (lifeWS2 <= 0)
        {
            weakSpot2.SetActive(false);
        }

        if (lifeWS3 <= 0)
        {
            weakSpot3.SetActive(false);
            Destroy(weakSpot3);

            Debug.Log("DeSTROY");
            
        }




    }

    private void MindAttack01()
    {
        Debug.Log("A01");
        BodyAttack01();
        timeA1 += Time.deltaTime;

        if (timeA1 >= minimumTimeAttack)
        {
            timeA1 = 0.0f;
            minimumTimeA1Finish = true;
        }

        //When one weakspot has been destroyed

        if(minimumTimeA1Finish && (lifeWS1<=0||lifeWS2<=0))
        {
            minimumTimeA1Finish = false;
            canMove = false;
            current_mind_state_ = MindStates.kAttack02;
        }
        

        if (a2BOOL)
        {
            canMove = false;
            current_mind_state_ = MindStates.kAttack02;
            a2BOOL = false;
        }

        if (a3BOOL)
        {
            canMove = false;
            current_mind_state_ = MindStates.kAttack03;
            a3BOOL = false;
        }
    }

    private void MindAttack02()
    {
        Debug.Log("A02");
        BodyAttack02();
        timeA2 += Time.deltaTime;

        if (timeA2 >= minimumTimeAttack)
        {
            timeA2 = 0.0f;
            minimumTimeA2Finish = true;
        }
        //When the other weak point has been destroyed 
        if (minimumTimeA2Finish && (lifeWS2 <= 0  && lifeWS1<=0) )
        {
            minimumTimeA2Finish = false;
            canMove = false;
            current_mind_state_ = MindStates.kAttack03;
        }else if (minimumTimeA2Finish)
        {
            current_mind_state_ = MindStates.kAttack01;
        }

        

        if (a1BOOL)
        {
            canMove = false;
            current_mind_state_ = MindStates.kAttack01;
            a1BOOL = false;
        }

        if (a3BOOL)
        {
            canMove = false;
            current_mind_state_ = MindStates.kAttack03;
            a3BOOL = false;
        }

    }



    private void MindAttack03()
    {
        BodyAttack03();

       


        if (a2BOOL)
        {
            canMove = false;
            current_mind_state_ = MindStates.kAttack02;
            a2BOOL = false;
        }

        if (a1BOOL)
        {
            canMove = false;
            current_mind_state_ = MindStates.kAttack01;
            a1BOOL = false;
        }
    }

    
 

    

    private void MindWait()
    {
        if (isActive)
        {
            countDowndSecondWait += Time.deltaTime;

            if (countDowndSecondWait >= secondwait)
            {
                
                current_mind_state_ = MindStates.kAttack01;
            }

           
        }
    }


    //BODY
    private void BodyAttack01()
    {

        

        if (lookingUP)
        {
            canMove = false;
           
            
            gameObject.transform.Rotate(0.0f, 0.0f, 10.0f * Time.deltaTime * rotationSpeed, Space.Self);


            if (gameObject.transform.rotation.z >= stopLeft)
            {
                Debug.Log("stopppppppp");

                
                lookingUP = false;
                lookingLeft = true;
                canMove = true;
                
            }

        }else if (lookingRight)
        {
           
            gameObject.transform.Rotate(0.0f, 0.0f, -10.0f * Time.deltaTime * rotationSpeed, Space.Self);

            if (gameObject.transform.rotation.z <= stopLeft)
            {
               


                
                
                lookingLeft = true;
                lookingRight = false;
            }
        }
        else
        {
            canMove = true;
        }



       



        if (canShoot & isActive)
        {
            Shoot();
        }

        if (canMove)
        {
            //Movement
            if (gameObject.transform.position.y >= limitUp)
            {
                Debug.Log("MOVED");

                goingUp = false;
                goingDown = true;
            }


            if (gameObject.transform.position.y <= limitDown)
            {
                Debug.Log("MOVEU");
                goingDown = false;
                goingUp = true;
            }



            if (goingDown)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }

            if (goingUp)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
        }









    }

    private void BodyAttack02()
    {
        //Debug.Log("A2");

        


        if (lookingLeft)
        {
          
            gameObject.transform.Rotate(0.0f, 0.0f, -10.0f * Time.deltaTime * rotationSpeed, Space.Self);

            if (gameObject.transform.rotation.z <= stopUP)
            {
                Debug.Log("stopppppppp");

              
                shootThunder = true;
                lookingUP = true;
                lookingLeft = false;
                lookingRight = false;
            }
        }



        if (lookingRight)
        {
           
            gameObject.transform.Rotate(0.0f, 0.0f, -10.0f * Time.deltaTime * rotationSpeed, Space.Self);

            if (gameObject.transform.rotation.z <= stopUP)
            {
                Debug.Log("stopppppppp");

               
                shootThunder = true;
                lookingUP = true;
                lookingLeft = false;
                lookingRight = false;
            }
        }








        if (shootThunder && !thunderTriggerInstantiated)
        {
            Instantiate(thunderTrigger, throwpoint.position, Quaternion.identity);

            thunderTriggerInstantiated = true;
            shootThunder = false;



        }

        Debug.Log(thunderTime);
        if (thunderTriggerInstantiated)
        {
           
            thunderTime += Time.deltaTime;
           
            if (thunderTime >= 3.0f)
            {
               
               
                thunderTriggerInstantiated = false;
                thunderAnimator.SetTrigger("runThunder");
                thunderTime = 0.0f;
            }
        }

       


       

       

    }









    private void BodyAttack03()
    {



        if (!lookingRight)
        {
            
            gameObject.transform.Rotate(0.0f, 0.0f, 10.0f * Time.deltaTime * rotationSpeed, Space.Self);

            if (gameObject.transform.rotation.z >=  stopRight)
            {
                Debug.Log("stopppppppp");

               
               
                lookingRight = true;
                lookingUP = false;
                lookingLeft = false;
                canMove = true;
            }
        }
       





        if (canMove)
        {
            //Movement
            if (gameObject.transform.position.y >= limitUp)
            {
                



                
                goingDown = false;
                goingUp = true;
            }


            if (gameObject.transform.position.y <= limitDown)
            {
                

                goingUp = false;
                goingDown = true;
            }



            if (goingDown)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }

            if (goingUp)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            beamtime += Time.deltaTime;

            if (beamtime >= 5.0f)
            {
                Debug.Log("BEEEEAM");
                beamAnimator.SetTrigger("beam");
                beamtime = 0.0f;
                slowTime = true;
                


                t += Time.deltaTime;
                if (t > 2.0f)
                {
                    speed = 2.0f;
                }

                if (t > 4.0f)
                {
                    speed = startSpeed;
                    t = 0.0f;
                }



              
               

            }

            if (slowTime)
            {
               
                t2 += Time.deltaTime;
                //Debug.Log(t2);

                if (t2 >= 2.5f)
                {
                    Debug.Log("slow");
                    speed = 3.0f;
                }


                if (t2 >= 4.3f)
                {
                    speed = startSpeed;
                    t2 = 0.0f;
                    slowTime = false;
                }


                

            }








        }






      









    }

   
   


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Solo reciben daño los "Weak Spot"

        switch (collision.otherCollider.gameObject.tag)
        {

            case "ws1":

                lifeWS1--;



                break;

            case "ws2":

                lifeWS2--;

                Debug.Log("aahi2");

                break;


            case "ws3":

                lifeWS3--;

                Debug.Log("aahi");

                break;
        }
        


    }



}
