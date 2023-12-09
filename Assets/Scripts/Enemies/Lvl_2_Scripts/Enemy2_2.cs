using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_2 : EnemyBase
{
    public Transform point;
    public GameObject body;

    // Start is called before the first frame update
    private void Start()
    {
        FindPlayer();
        GetCollider();
    }
    // Update is called once per frame
    void Update()
    {

        //Life
        if (life <= 0)
        {

            body.SetActive(false);
            isActive = false;
        }

        //Activation
        DisableEnemy();
        DistacePlayer();
        ActiveEnemy();

        if (isActive)
        {
            body.transform.RotateAround(point.position, Vector3.forward, speed * Time.deltaTime);
        }

       
        
        
    }

   
}
