using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1_5 : EnemyBase
{


    public float limitUp;
    public float limitDown;
    private bool goingDown;
    private bool goingUp;
    private float startSpeed;
    private float timelimit = 8.0f;
    private float timer;
    private float slowPoint;
    public float submarinelife=3.0f;
    public bool spawned;
    private bool isactive;

    public delegate void bossEventHandler();
    public event bossEventHandler bossEvent;
    private void Start()
    {
        FindPool();
      
        canShoot = true;
        startSpeed = speed;
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.active)
        {
            isactive = true;
        }
        Debug.Log("player:"+playerPos.transform.position);
        if (submarinelife <= 0)
        {
            gameObject.SetActive(false);
        }
        slowPoint = playerPos.position.x + 5.0f;
        if (transform.position.x >= slowPoint && timer>=timelimit)
        {
            speed = startSpeed - 1.0f;
        }
        //Activation
       
        DistacePlayer();

        ActiveEnemy();

        //Mov
        if (isActive)
        {
            timer += Time.deltaTime;
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);


            //Movement Y
            if (gameObject.transform.position.y >= limitUp)
            {

                goingUp = false;
                goingDown = true;
            }


            if (gameObject.transform.position.y <= limitDown)
            {

                goingDown = false;
                goingUp = true;
            }

            if (goingDown)
            {
                GoingDown();
            }

            if (goingUp)
            {
                GoingUp();
            }



        }

        


    }
    private void LateUpdate()
    {

        //Activation
        if (canShoot & isActive)
        {
            Shoot();
        }

    }


    private void GoingDown()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void GoingUp()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "sword")
        {

            submarinelife--;
        }
    }

    private void OnDisable()
    {
        if (isactive)
        {
            if (bossEvent != null)
            {

                bossEvent();
            }

        }
      
    }

    private void OnEnable()
    {
        FindPlayer();
        transform.position = new Vector3(playerPos.transform.position.x+10.0f, gameObject.transform.position.y, gameObject.transform.position.z);

    }
    
    

}
