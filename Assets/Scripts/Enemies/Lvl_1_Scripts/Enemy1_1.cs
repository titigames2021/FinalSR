using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_1 : EnemyBase
{

    //The enemy moves around Y axis and shoot 


    public float limitUp;
    public float limitDown;
    private bool goingDown;
    private bool goingUp;
    private bool repeatShoot;
    


    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
        FindPool();
        goingUp = true;
        canShoot = true;

        GetAnimator();

        GetCollider();
    }

    // Update is called once per frame
    void Update()
    {
       
        CheckLife();
        DistacePlayer();
        DisableEnemy();
        ActiveEnemy();

     


        if (isActive)
        {
            //Movement
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


    







    public void NewShoot()
    {

       if(isActive)
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

   
}
