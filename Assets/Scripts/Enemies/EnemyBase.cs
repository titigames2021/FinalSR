using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float life;
    public float speed; 
    protected bool canShoot;
    protected GameObject obj;
    public Transform throwpoint;
    public float projectailSpeed;
    public ObjectPoolerScript enemiePool;
    protected Transform playerPos;
    protected float playerDis;
    public float activationDistance;
    public float coolDownTime;
    public bool isActive;
    protected float disableDistance=-30.0f;
    //public float lifeTime;
    //public float lifeTimeLimit;
    public LayerMask playerproj;
    public LayerMask enemyLayer;
    public GameObject[] PowerUps;
    public Transform playerShip;


    public void Disable()
    {
        gameObject.SetActive(false);
    }




    protected void FindPool()
    {
        //Esto creo que va a consumir mucho 
        GameObject pool = GameObject.FindWithTag("EnemyPool");
        enemiePool  = pool.GetComponent<ObjectPoolerScript>();

    }


    protected void FindPlayer()
    {
        //cuidao que he cambiado el player por el scrolls
        GameObject player = GameObject.FindWithTag("MainCamera");
        playerPos = player.transform;
    }

    protected void FindPlayerShip()
    {
        //cuidao que he cambiado el player por el scrolls
        GameObject player = GameObject.FindWithTag("Player");
        playerShip = player.transform;
    }
    protected void CheckLife()
    {
        if (life <= 0)
        {
           
            int randomNum = UnityEngine.Random.Range(0, PowerUps.Length);
            isActive = false;
            
            gameObject.SetActive(false);
            CancelInvoke();
            Vector3 pos = gameObject.transform.position;

            if (PowerUps.Length > 0)
            {
                Instantiate(PowerUps[randomNum], pos, Quaternion.identity);
            }

            
        }
    }


    protected void ActiveEnemy()
    {
        if (playerDis <= activationDistance)
        {
            isActive = true;
        }
        
    }


    protected void DistacePlayer()
    {
        playerDis=gameObject.transform.position.x- playerPos.position.x;

        
    }

    protected void DisableEnemy()
    {
        //Creo que es malo para el rendimiento que todos los enemigos esten calculando a la vez su distancia con el player 
        
        if (playerDis <= disableDistance && isActive)
        {
            gameObject.SetActive(false);
        }
        


        //Creo que esto consume menos, pero para algunos enemigos no sirve , PERO PARA ACTIVARLO NO SIRVE 
        /*
        lifeTime += Time.deltaTime;

        if (lifeTime >=lifeTimeLimit)
        {
            gameObject.SetActive(false);
        }

        */


    }

    protected void Shoot()
    {
        canShoot = false;

        obj = enemiePool.GetPooledObject();
        if (obj == null) return;

        obj.transform.position = throwpoint.position;
        obj.transform.rotation = throwpoint.rotation;
        obj.SetActive(true);



     

        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
        objRb.AddForce(-obj.gameObject.transform.right * projectailSpeed, ForceMode2D.Impulse);
        Invoke("ResetCooldown", coolDownTime);
    }

    private void ResetCooldown()
    {
        // Reiniciar el indicador de si se puede lanzar un objeto
        canShoot = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(LayerMask.LayerToName(gameObject.layer) == "Enemy")
        {
            if (playerproj == (playerproj | (1 << collision.gameObject.layer)))
            {
                life--;
                if (collision.gameObject.tag == "downbullet")
                {
                    life = life - 4;
                }
            }
        }
       
    }





    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "sword")
        {
           
            life--;
        }
    }
}
