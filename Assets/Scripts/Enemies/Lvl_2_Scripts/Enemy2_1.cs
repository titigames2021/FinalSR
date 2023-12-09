using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_1 :EnemyBase
{
    public Vector3 sumV = new Vector3(0.0f, 10.0f, 0.0f);
    public float more;
    // Start is called before the first frame update
    void Start()
    {
        FindPool();
        FindPlayer();
        FindPlayerShip();
        canShoot = true;
        _animator = GetComponent<Animator>();
        GetCollider();
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

        if (isActive)
        {
            //Aim to the player 
            //transform.up = playerPos.position - transform.position;
            

            // Rotar hacia el jugador sin cambiar el eje Z
            Vector3 direction = playerShip.position - transform.position;
            direction.z = 0f;
            transform.up = direction+sumV;
         

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.red);

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
   
}

