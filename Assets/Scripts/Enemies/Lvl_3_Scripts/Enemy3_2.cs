using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_2 : EnemyBase
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
    public bool upVariant;
    public bool downVariant;
    public Transform throwpointUp;
    public Transform throwpointDown;

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
            ShootD();
        }

    }

    private void MindShoot()
    {

        if (stop == false)
        {
            current_mind_state_ = MindStates.kPursuit;
        }

    }

    private void MindPursuit()
    {
        BodyPursuit();
        if (stop)
        {
            current_mind_state_ = MindStates.kShoot;
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
        

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(target.x, currentPosition.y, currentPosition.z);
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, step);



    }


    public void ShootD()
    {
       
        
            canShoot = false;

            obj = enemiePool.GetPooledObject();
            if (obj == null) return;



        if (upVariant)
        {
            obj.transform.position = throwpointUp.position;
            obj.transform.rotation = throwpointUp.rotation;
        }else if (downVariant)
        {
            obj.transform.position = throwpointDown.position;
            obj.transform.rotation = throwpointDown.rotation;
        }
            
            obj.SetActive(true);





            Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
            Vector2 projectileVelocity = new Vector2(objRb.velocity.x + 5, objRb.velocity.y);
            objRb.velocity = projectileVelocity;
            objRb.AddForce(-obj.gameObject.transform.right * projectailSpeed, ForceMode2D.Impulse);
            Invoke("ResetCooldown", coolDownTime);

       


    }

}
