using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_2 : EnemyBase
{
    private void Start()
    {
        FindPool();
        FindPlayer();
        canShoot = true;
        GetCollider();
        GetAnimator();

    }


    // Update is called once per frame
    void Update()
    {
     //Life
        CheckLife();
        //Activation
        DisableEnemy();
        DistacePlayer();

        ActiveEnemy();

        //Mov
        if (isActive)
        {
    
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
       
    }


    public void NewShoot()
    {
        if(isActive)
        {
            Shoot();

        }

        

    }

}
