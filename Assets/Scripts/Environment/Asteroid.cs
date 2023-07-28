using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyBase
{
    



    private void Update()
    {
        FindPlayer();
        DistacePlayer();
        ActiveEnemy();
       
        if (isActive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);


            CheckLife();
        }
       

        
    }







}
