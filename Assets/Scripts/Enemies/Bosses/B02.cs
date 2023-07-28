using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B02 : EnemyBase
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
 

    public GameObject weakSpot1;
    public GameObject weakSpot2;
   

     public int lifeWS1 = 8;
    public int lifeWS2 = 8;
    public int lifeWS3 = 8;



 

    public bool canMove;
    
  


    public float timeToChange;
  

    private float startSpeed;

    public Sprite sprite2;
    public SpriteRenderer spriteR;

    //1ºAttack
    public bool r;
    public bool l;


    public float displacement;
    public float displacementLimit;
    public float startX;
    private Vector3 startpos;

    public Transform limitL;
    public Transform limitR;
   //2ºAttack

    public bool canShoot2;
    private bool doWarning;
    //3ºAtack
    public Animator warning;
    public Animator quake;
    public bool activated;
    private bool repeatA03;
   
    public int times;
    private bool firstmove;

    public bool quakefinished;
    public bool activationfinished;
    public bool quake_b;
    public bool warning_b;
    private bool finishA03;
    public float tquake;
    private bool quakeStarted;

    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();

        canShoot = true;


        startSpeed = speed;

        startX = transform.position.x;
        startpos = transform.position;
        r = true;
        repeatA03 = true;
        warning_b = true;


        spriteR = gameObject.GetComponent<SpriteRenderer>();
        
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




    }


    private void MindWait()
    {
        if (isActive)
        {
            countDowndSecondWait += Time.deltaTime;

            if (countDowndSecondWait >= secondwait)
            {
              
                countDowndSecondWait = 0.0f;
                current_mind_state_ = MindStates.kAttack01;
            }


        }
    }


    private void MindAttack01()
    {
        BodyAttack01();
        countDowndSecondWait += Time.deltaTime;

        if (countDowndSecondWait >= timeToChange)
        {
            // no estara mal añadir algo para que vuelva a la pos inicial pero parece que no hace falta
            countDowndSecondWait = 0.0f;
            current_mind_state_ = MindStates.kAttack02;
            r = true;
            l = false;
        }

    }

    private void MindAttack02()
    {
        BodyAttack02();
       
        countDowndSecondWait += Time.deltaTime;

        

        if (countDowndSecondWait >= timeToChange)
        {
            countDowndSecondWait = 0.0f;
            current_mind_state_ = MindStates.kAttack03;
        }
    }



    private void MindAttack03()
    {
        BodyAttack03();
       
        
        if (tquake>=4.5f)
        {
            warning_b = true;
            quakeStarted = false;
            tquake = 0.0f;
            current_mind_state_ = MindStates.kAttack01;
        }
        
    }







    //BODY
    private void BodyAttack01()
    {
        Debug.Log("Atacando1");

        // Coje carrerilla hacia la derecha(r)
        //Se abalanza sobre el player hacia la izquierda(l)
        if (l)
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);


        }
        else if (r)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        //displacement = Mathf.Abs(startX - transform.position.x);
        if (transform.position.x>=limitR.position.x)
        {
            r = false;
            l = true;

        }

        if (transform.position.x <= limitL.position.x)
        {
            r =true;
            l =false;

        }


    }

    private void BodyAttack02()
    {
        Debug.Log("Atacando2");
        //Volver a la posicion inicial
        Vector3 nuevaPosicion = Vector3.MoveTowards(transform.position, startpos, speed * Time.deltaTime);

        // Mueve el objeto hacia la nueva posición
        transform.position = nuevaPosicion;

        if (transform.position == startpos)
        {
            canShoot2 = true;
;        }

        //Disparar un proyectil especial que se dirige hacia el enemigo



        if (canShoot & isActive && canShoot2)
        {
            Shoot();
        }
       

    }









    private void BodyAttack03()
    {
        Debug.Log("Atacando3");

        if (transform.position.x >= startX)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        

        

        if (warning_b)
        {
            warning.SetBool("Warning", true);
            
        }


        if (warning_b == false)
        {
            warning.SetBool("Warning", false);
        }



        if (quake_b)
        { 
            warning_b = false;
            Debug.Log("HOSTIAAAAAJUAN");
            quake.SetTrigger("quake");
            quakeStarted = true;
            tquake+=Time.deltaTime;

        }


        if (quakeStarted)
        {
            tquake += Time.deltaTime;
        }
        
     








    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Solo reciben daño los "Weak Spot"

        switch (collision.otherCollider.gameObject.tag)
        {

            case "2ws1":
                Debug.Log("pumpum");
                lifeWS1--;
                if (lifeWS1 <= 0)
                {
                    Destroy(weakSpot1);
                    spriteR.sprite = sprite2;
                    
                }


                break;

            case "2ws2":

                Debug.Log("aaaaaaa");
                lifeWS2--;

                if (lifeWS2 <= 0)
                {
                    gameObject.SetActive(false);
                }

                break;


           
        }



    }
}
