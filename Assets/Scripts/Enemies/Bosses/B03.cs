using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B03 : EnemyBase
{
    public int lifeWS1 = 8;
    public int lifeWS2 = 8;
    public float lifeWS3 = 10.0f;
    public GameObject WS1;
    public GameObject WS2;
    public GameObject WS3;
    public Transform[] limits;   //Left Rigth  Down Up
    private bool isactive;

    public delegate void bossDefeatEventHandler();
    public event bossDefeatEventHandler bossDefeatEvent;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DistacePlayer();
        ActiveEnemy();
        if (isActive)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (gameObject.active)
        {
            isactive = true;
        }




    }

    private void LateUpdate()
    {
        if (isActive)
        {

            //limits of the player's area of movement
            gameObject.transform.position = new Vector3(

                Mathf.Clamp(transform.position.x, limits[0].transform.position.x, limits[1].transform.position.x),
                Mathf.Clamp(transform.position.y, limits[2].transform.position.y, limits[3].transform.position.y),
                transform.position.z





                );

        }
     
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Solo reciben daño los "Weak Spot"

        switch (collision.otherCollider.gameObject.tag)
        {
            //Cuando le eliminan el primer WeakSpot baja la velocidad y se posiciona en el primer limiteL 
            //Cuando le eliminan el degundo WeakSpot se cambia el limiteLeft por otro mas a la izq 
            case "3ws1":
                Debug.Log("pumpum");
                lifeWS1--;
                if (lifeWS1 <= 0)
                {
                    speed = 0.0f;
                    Destroy(WS1);
                }


                break;

            case "3ws2":

                lifeWS2--;

                if (lifeWS2 <= 0)
                {
                    Destroy(WS2);
                    limits[0] = limits[4];
                }

                break;



            



        }





    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "sword")
        {

            lifeWS3--;
            if (lifeWS3 <= 0)
            {
                Debug.Log("killed");
                gameObject.SetActive(false);

            }
        }
    }

    private void OnEnable()
    {
        FindPlayer();
        transform.position = new Vector3(playerPos.transform.position.x + 30.0f, gameObject.transform.position.y, gameObject.transform.position.z);

    }

    private void OnDisable()
    {
        if (isactive)
        {
            if (bossDefeatEvent != null)
            {

                bossDefeatEvent();
            }

        }
    }


}
