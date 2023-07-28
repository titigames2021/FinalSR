using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_3 : EnemyBase
{

    //Mix of Enemy1_1 and Enemy1_2, without shooting 

    public float limitUp;
    public float limitDown;
    private bool goingDown;
    private bool goingUp;


    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
        goingUp = true;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Life
        DisableEnemy();
        CheckLife();
        //Activation
        DistacePlayer();
        ActiveEnemy();

        if (isActive)
        {
            //Debug.Log(gameObject.transform.position.y);
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

            //Movement X
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
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
}
