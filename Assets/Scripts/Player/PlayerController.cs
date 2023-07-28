using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameObject pause_UI;
    public SpriteRenderer spriteR;
    public Sprite startSprite;

    //UI
   
    private bool pause_input;
    public GameObject energyBar;
    public float currentEnergy;
    private float energyMax = 100.0f;
    public float life;
    public GameObject energyUI;

    //INPUT

    //Movement
    private float smooth_factor = 0.2f;
    private PlayerInput _input;
    private Vector2 move_input;
    private CharacterController controller;
    public float playerSpeed;
    public Transform limitXL;
    public Transform limitXR;
    public Transform limitUp;
    public Transform limitDown;
   
    //Shoot
   
    private bool shoot_input;
    private bool canShoot;
   
    
    public float speedP;// Projectail speed at start 
    private bool charging;
    private bool canShootCharged;
    private bool ShootCharged;
    public bool dobleShoot;
    public bool tripleShoot;
    public bool verticalShoot;

    public ObjectPoolerScript poolCharged;
    private GameObject pool3_obj;
    public ObjectPoolerScript pool3;
    private BoxCollider2D backcol;
    private GameObject playerpool_obj;
    public ObjectPoolerScript basicPool;
    private GameObject chargedpool_obj;
    public Transform[] throwpoints;// basic/vertical/double/triple


    private GameObject obj;
    private GameObject obj2;
    private GameObject obj3;
    private GameObject objv1;
    private GameObject objv2;


    public bool oneShoot;






    

    public LayerMask powerUpLayer;
    public LayerMask ownBullets;
    public LayerMask swordlay;

    public float countdownMejora = 0.0f;
    public float cdmejoralimit = 3.5f;
    public bool usingMejora;

    //Wtime
    public bool witchTimeTrigger;
    public bool letsdance;
    public bool runWT;
    public float duration;
    public float witchTimeScale;
    public float playerSpeedWitch;
    public float normalPlayerSpeed;
    public float normalSpeedProjectail;
    public float witchSpeedProjectail;
    public float witchTimeDuration;
    private float chargeCount;
    private float limitchargeCount = 1.0f;
    private bool wtime_input;

   
    //ScaleDown

    public bool scaleDownTrigger;
    private bool scaleDown_input;
    private Vector3 startScale;
    private Vector3 smallScale;
    public bool scalingDown;
    private float SpeedEnergyScaleDown = 10.0f;


    //Sword
    public bool swordTrigger;
    public bool swordEnabled;
    public Collider2D back;
    public Sprite swordSprite;
    private bool sword_input;
    public Transform B03spawnPoint;
    public B03 _b03;

    private int currentLevel;

    public Vector2 impulse;
    private bool isPaused;
    private Collider2D basecoll;
    public Collider2D swordcoll;




    private Vector2 currentInputVector;
    public Vector2 smoothInputVelocity;
    public float smoothInputSpeed = .2f;
    private InputAction  moveAction;

    private void Awake()
    {
        //INPUT BINDING

        //Movement

        
        _input = new PlayerInput();

        /*
        
        _input.General.Movement.performed += move_performed =>
        {

            move_input = move_performed.ReadValue<Vector2>();
        };

        _input.General.Movement.canceled += move_performed =>
        {


            move_input = move_performed.ReadValue<Vector2>();
        };
       */
        
        //Shoot

        _input.General.Shoot.performed += shoot_performed =>
        {
            if (canShoot)
            {
                shoot_input = shoot_performed.ReadValueAsButton();
              
            }
            
        };

        _input.General.Shoot.canceled += shoot_performed =>
        {
            canShoot = true;
            shoot_input = shoot_performed.ReadValueAsButton();
            charging = false;
            chargeCount = 0.0f;
            if (canShootCharged)
            {
               
                ShootCharged = true;
            }
        };


        //Witch Time

        _input.General.WitchTime.performed += wtime_performed =>
        {



            if (!usingMejora)
            {
                wtime_input = wtime_performed.ReadValueAsButton();

                if (witchTimeTrigger && letsdance && currentEnergy > 0.0f)
                {
                    runWT = true;
                    currentEnergy = currentEnergy - 30.0f;
                    usingMejora = false;
                }
            }
               






        };


        _input.General.WitchTime.canceled += wtime_performed =>
        {
           
            wtime_input = wtime_performed.ReadValueAsButton();
        };


        //ScaleDown

        _input.General.ScaleDown.performed += scaleDown_performed =>
        {


            if (!usingMejora) { 
                
                scaleDown_input = scaleDown_performed.ReadValueAsButton();

                if (scaleDownTrigger && currentEnergy > 0.0f)
                {
                    scalingDown = true;
                    usingMejora = true;
                    currentEnergy = currentEnergy - 30.0f;
                }

            }




        };

        _input.General.ScaleDown.canceled += scaleDown_performed =>
        {

            scaleDown_input = scaleDown_performed.ReadValueAsButton();
        };



        //ActiveSword

        _input.General.Sword.performed += sword_performed =>
        {
            if (!usingMejora)
            {

               
                sword_input = sword_performed.ReadValueAsButton();

                if (swordTrigger && currentEnergy > 0.0f)
                {
                    swordEnabled = true;
                    usingMejora = true;
                    currentEnergy = currentEnergy - 30.0f;
                }


            }
                


            


        };

        _input.General.Sword.canceled += sword_performed =>
        {

            sword_input = sword_performed.ReadValueAsButton();
        };

        //Pause

        _input.General.Pause.performed += pause_performed =>
        {

            pause_input = pause_performed.ReadValueAsButton();

            if (GameManager.Instance.isPaused)
            {
                GameManager.Instance.ResumeGame();
                ResumeUI();
                canShoot = true;
                Cursor.visible = false;
            }
            else
            {
                GameManager.Instance.PauseGame();
                PauseUI();
                canShoot = false;
                Cursor.visible = true;
            }

            

        };



        







    }


    // Start is called before the first frame update
    void Start()
    {
       pause_UI = GameObject.FindWithTag("pauseui");
        pause_UI.SetActive(false);

        energyBar = GameObject.FindWithTag("currentenergybar");

        energyUI = GameObject.FindWithTag("energyui");


       


        playerpool_obj = GameObject.FindWithTag("playerpool");

        basicPool = playerpool_obj.gameObject.GetComponent<ObjectPoolerScript>();

        chargedpool_obj = GameObject.FindWithTag("playerpoolcharged");
        poolCharged = chargedpool_obj.gameObject.GetComponent<ObjectPoolerScript>();

        pool3_obj = GameObject.FindWithTag("playerpool3");

        pool3 = pool3_obj.gameObject.GetComponent<ObjectPoolerScript>();

        backcol  = gameObject.GetComponent<BoxCollider2D>();

        swordcoll.enabled = false;

        // int layerToIgnoreIndex = LayerMask.NameToLayer("PlayerProjectile");
        // Physics.IgnoreLayerCollision(gameObject.layer, 3);

        spriteR = gameObject.GetComponent<SpriteRenderer>();

        startSprite = spriteR.sprite;





        startScale = gameObject.transform.localScale;

        smallScale = startScale / 2.0f;
        

        canShoot = true;

        normalPlayerSpeed = playerSpeed;
        normalSpeedProjectail = speedP;


        if (GameManager.Instance.level1completed)
        {
            witchTimeTrigger = true;


        }

        if (GameManager.Instance.level2completed)
        {
            scaleDownTrigger = true;


        }



        if (GameManager.Instance.level3completed)
        {
            swordTrigger = true;


        }





    }
    private void LateUpdate()
    {
        //limits of the player's area of movement
        gameObject.transform.position = new Vector3(

            Mathf.Clamp(transform.position.x, limitXL.transform.position.x, limitXR.transform.position.x),
            Mathf.Clamp(transform.position.y, limitDown.transform.position.y, limitUp.transform.position.y),
            transform.position.z





            );
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(usingMejora);
        //movement 

        Vector2 move_input = _input.General.Movement.ReadValue<Vector2>();

        //currentInputVector = Vector2.Lerp(currentInputVector, move_input, ref smoothInputVelocity, smoothInputSpeed);

         currentInputVector =  Vector2.Lerp(currentInputVector, move_input, smoothInputSpeed);
        Vector3 move = new Vector3(currentInputVector.x, currentInputVector.y, 0);

        transform.Translate(move * playerSpeed * Time.deltaTime);


       
        //improvised UI Managament

        float healthRatio = currentEnergy / energyMax;
        healthRatio = Mathf.Clamp(healthRatio, 0f, 3f);
        energyBar.transform.localScale = new Vector3(healthRatio, 1, 1);


       



        if (life <= 0)
        {
            Time.timeScale = 1;
            spriteR.enabled = false;
            swordEnabled = false;
            canShoot = false;
            Invoke("Reload", 2.0f);
        }





        //Movement





        //Debug.Log(witchTimes);
        /*
        if (move_input.x <= 0)
        {
            if (!runWT)
            {
                playerSpeed = normalPlayerSpeed + 2.0f;
            }
          
        }
        else 
        {
            playerSpeed = normalPlayerSpeed;
        }

      

        transform.Translate(move_input * playerSpeed * Time.deltaTime);
        */

        //Shooting using pooling objects 
        if (shoot_input && canShoot)
        {
            canShoot = false;
            charging = true;

           
           



            if (tripleShoot)
            {
                Debug.Log("3");
                //new projectails 
                obj2 = pool3.GetPooledObject();
                if (obj2 == null) return;


                obj3 = pool3.GetPooledObject();
                if (obj3 == null) return;


                obj = pool3.GetPooledObject();
                if (obj == null) return;


                //1

                obj.transform.position = throwpoints[5].position;
                obj.transform.rotation = throwpoints[5].rotation;
                obj.SetActive(true);

             



                Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
                objRb.AddForce(obj.gameObject.transform.right * speedP, ForceMode2D.Impulse);


                //2
                obj2.transform.position = throwpoints[6].position;
                obj2.transform.rotation = throwpoints[6].rotation;
                obj2.SetActive(true);





                Rigidbody2D objRb2 = obj2.GetComponent<Rigidbody2D>();
                objRb2.AddForce(obj2.gameObject.transform.right * speedP, ForceMode2D.Impulse);


                //3
                
                obj3.transform.position = throwpoints[7].position;
                obj3.transform.rotation = throwpoints[7].rotation;
                obj3.SetActive(true);





                Rigidbody2D objRb3 = obj3.GetComponent<Rigidbody2D>();
                objRb3.AddForce(obj3.gameObject.transform.right * speedP, ForceMode2D.Impulse);






            }else if (verticalShoot)
            {
                //Projectails
                    objv1 = pool3.GetPooledObject();
                    if (objv1 == null) return;


                    objv2 = pool3.GetPooledObject();
                    if (objv2 == null) return;



                    obj = basicPool.GetPooledObject();
                    if (obj == null) return;


                    //1

                    obj.transform.position = throwpoints[0].position;
                    obj.transform.rotation = throwpoints[0].rotation;
                    obj.SetActive(true);





                    Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
                    objRb.AddForce(obj.gameObject.transform.right * speedP, ForceMode2D.Impulse);


                    //V1

                   objv1.transform.position = throwpoints[1].position;
                    objv1.transform.rotation = throwpoints[1].rotation;
                    objv1.SetActive(true);

                    Rigidbody2D objRbv1 = objv1.GetComponent<Rigidbody2D>();
                    objRbv1.AddForce(objv1.gameObject.transform.right * speedP, ForceMode2D.Impulse);


                    //V2

                    objv2.transform.position = throwpoints[2].position;
                    objv2.transform.rotation = throwpoints[2].rotation;
                    objv2.SetActive(true);





                    Rigidbody2D objRbv2 = objv2.GetComponent<Rigidbody2D>();
                    objRbv2.AddForce(objv2.gameObject.transform.right * speedP, ForceMode2D.Impulse);










            }
            else if (dobleShoot)
            {
                Debug.Log("2");
                //New projectail
                obj2 = basicPool.GetPooledObject();
                if (obj2 == null) return;
                obj = basicPool.GetPooledObject();
                if (obj == null) return;

                //1
                obj.transform.position = throwpoints[3].position;
                obj.transform.rotation = throwpoints[3].rotation;
                obj.SetActive(true);

              



                Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
                objRb.AddForce(obj.gameObject.transform.right * speedP, ForceMode2D.Impulse);


                //2
                obj2.transform.position = throwpoints[4].position;
                obj2.transform.rotation = throwpoints[4].rotation;
                obj2.SetActive(true);





                Rigidbody2D objRb2 = obj2.GetComponent<Rigidbody2D>();
                objRb2.AddForce(obj2.gameObject.transform.right * speedP, ForceMode2D.Impulse);
            }
            else
            {

                obj = basicPool.GetPooledObject();
                if (obj == null) return;

                
                obj.transform.position = throwpoints[0].position;
                obj.transform.rotation = throwpoints[0].rotation;
                obj.SetActive(true);

                



                Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
                objRb.AddForce(obj.gameObject.transform.right * speedP, ForceMode2D.Impulse);

            }



            //Debug.Log("Velocidad actual: " + objRb.velocity.magnitude);



        }
        if (charging)
        {
            chargeCount += Time.deltaTime;
            //Debug.Log(chargeCount);
        }
        if (chargeCount >= limitchargeCount)
        {
           
            canShootCharged= true;
            chargeCount = 0.0f;
        }

        if (ShootCharged)
        {
            ShootCharged = false;
            canShootCharged = false;
            
          

            obj = poolCharged.GetPooledObject();
            if (obj == null) return;

            obj.transform.position = throwpoints[8].position;
            obj.transform.rotation = throwpoints[8].rotation;
            obj.SetActive(true);

            Debug.Log("ShootCharged");



            Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
            objRb.AddForce(obj.gameObject.transform.right * speedP, ForceMode2D.Impulse);

        }






       

        //WitchTime
     
        if (runWT)
        {
            Debug.Log("lets dance");
            //audioS.PlayOneShot(wtSound);
            Debug.Log(duration);
            duration += Time.deltaTime;
            Time.timeScale = witchTimeScale;
            playerSpeed = playerSpeedWitch;
            //basicAmmo.speed = ammoSpeed * 2;
            speedP = witchSpeedProjectail;

        }
        else
        {

            playerSpeed = normalPlayerSpeed;
            //basicAmmo.speed = ammoSpeed;
            speedP=normalSpeedProjectail;
        }


        if (duration >= witchTimeDuration)
        {
            Time.timeScale = 1.0f;
            duration = 0;
            runWT = false;
           // usingMejora = false;
        }

        //ScaleDown

        if (scalingDown)
        {
           
            gameObject.transform.localScale = smallScale;


           
            countdownMejora += Time.deltaTime;


            if (countdownMejora >= cdmejoralimit)
            {
                scalingDown = false;
                gameObject.transform.localScale = startScale;
                countdownMejora = 0.0f;
                usingMejora = false;
            }

        }

        //Sword

        if (swordEnabled)
        {
            backcol.enabled = false;
            spriteR.sprite = (swordSprite);
            swordcoll.enabled = true;
           
            countdownMejora += Time.deltaTime;
           
            if (countdownMejora >= cdmejoralimit)
            {
                swordEnabled = false;
                countdownMejora = 0.0f;
                usingMejora = false;

            }
        }
        else
        {
            spriteR.sprite = (startSprite);
            backcol.enabled = true;
            swordcoll.enabled = false;
        }

        if (currentLevel == 3)
        {
            if (transform.position.x >= B03spawnPoint.position.x)
            {
                _b03.gameObject.SetActive(true);
            }

        }

    }
    //WitchTimeFunctions
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        letsdance = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        letsdance = false;
    }


    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        


        
        if(!(ownBullets == (ownBullets | (1 << collision.gameObject.layer))))
        {
            life--;
        }

       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (!(powerUpLayer == (powerUpLayer | (1 << collision.gameObject.layer))) && !(ownBullets == (ownBullets | (1 << collision.gameObject.layer))) && collision.otherCollider.gameObject.tag == "sword")
        {
            life--;
        }









    }



    public void Reload()
    {

        GameManager.Instance.StartLevel(SceneManager.GetActiveScene().buildIndex);
        
    }


    public void PauseUI()
    {
      

        pause_UI.SetActive(true);
    }

    public void ResumeUI()
    {
        GameManager.Instance.ResumeGame();
        pause_UI.SetActive(false);
    }

    public void ExitUI()
    {
        GameManager.Instance.Exit();
    }


   

    private Vector2 SmoothStep(Vector2 value)
    {
        return new Vector2(Mathf.SmoothStep(0, 1, value.x), Mathf.SmoothStep(0, 1, value.y));
    }
}
