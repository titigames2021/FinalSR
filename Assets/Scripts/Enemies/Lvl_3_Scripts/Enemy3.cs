using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyBase
{

    //States 
    public MindStates current_mind_state_;
    public enum MindStates
    {
        kWait,
        kPursuit,
        kShoot
    }

    private Vector3 target;
    public float shoot_distance = 0.0f;
    public bool stop;
    public Vector3 distancePlayer;
    public Transform endPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        FindPool();

        FindPlayer();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
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
            else if (current_mind_state_ == MindStates.kPursuit)
            {
                MindPursuit();
            }
            else if (current_mind_state_ == MindStates.kShoot)
            {
                MindShoot();
            }
        
       


        //Target
        target = playerPos.position - distancePlayer;
        
    }

    private void LateUpdate()
    {

        //Activation
        if (canShoot & isActive)
        {
            Shoot();
        }

    }

    private void MindShoot()
    {
       
        if (stop==false)
        {
            current_mind_state_ = MindStates.kPursuit;
        }

    }

    private void MindPursuit()
    {
        BodyPursuit();
        if (playerPos.position.x >= endPoint.position.x)
        {
            canShoot = false;
            speed = 0.0f;
            Invoke("Disable", 2.0f);
        }
        
    }

    private void MindWait()
    {

        if (isActive)
        {
            current_mind_state_ = MindStates.kPursuit;
        }
    }



    private void BodyPursuit()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        

    }


    
}
