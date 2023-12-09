using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_5 : EnemyBase
{
    public bool leftSide;
    public bool rightSide;

    public Transform throwpointLeft;
    public Transform throwpointRight;

    public GameObject GunBarrelL;
    public GameObject GunBarrelR;


    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
        FindPool();
        canShoot = true;
        leftSide = true;
        GetCollider();
    }
    private void LateUpdate()
    {

        //Activation
        if (canShoot & isActive)
        {
            Shoot();
        }

    }
    // Update is called once per frame
    void Update()
    {
        DisableEnemy();
        DistacePlayer();
        ActiveEnemy();
        CheckLife();
        

        if(playerPos.position.x>= transform.position.x)
        {
            leftSide = false;
            rightSide = true;
        }
        else
        {
            leftSide = true;
            rightSide = false;
        }


        if (rightSide)
        {

            throwpoint = throwpointRight;
            GunBarrelL.SetActive(false);
            GunBarrelR.SetActive(true);

        }
        else if (leftSide)
        {
            throwpoint = throwpointLeft;
            GunBarrelR.SetActive(false);
            GunBarrelL.SetActive(true);
        }






    }

    

}
