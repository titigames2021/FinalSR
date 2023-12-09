using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

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
    public bool isDead;
    protected float disableDistance=-30.0f;
    //public float lifeTime;
    //public float lifeTimeLimit;
    public LayerMask playerproj;
    public LayerMask enemyLayer;
    public GameObject[] PowerUps;
    public Transform playerShip;
    public Animator _animator;
    private float testn;
    private bool isExplode;
    private bool b_powerUpDroped;
    public Collider2D enemyColl;
    


     public void GetCollider()
    {

        enemyColl = GetComponent<Collider2D>();
    }

    public void GetAnimator()
    {
        _animator = GetComponent<Animator>();
    }

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

           StartCoroutine(ExplodeAnimation());

            int randomNum = UnityEngine.Random.Range(0, PowerUps.Length);
                isActive = false;
              
                CancelInvoke();
                Vector3 pos = gameObject.transform.position;
                pos.y = pos.y -1.5f;
                pos.x = pos.x + 2.5f;


            if (PowerUps.Length > 0 && !b_powerUpDroped)   
                {
                    Instantiate(PowerUps[randomNum], pos, Quaternion.identity);
                    b_powerUpDroped = true;
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


    public void OnDisable()
    {
        
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "sword")
        {
           
            life--;
        }
    }





    // Corutina para ejecutar la animación "Explode".
    private IEnumerator ExplodeAnimation()
    {
        // Marca al personaje como muerto para evitar que esta corutina se ejecute más de una vez.
        isDead = true;

        //Ejecuta el trigger "Explode".
        _animator.SetTrigger("Explode");
        enemyColl.isTrigger = true; 

        //HAY QUE DESACTIVAR EL COLLIDER 


        // Puedes agregar un tiempo de espera (por ejemplo, 2 segundos) antes de realizar otras acciones.
        yield return new WaitForSeconds(.4f);

        // Aquí puedes realizar otras acciones después de la explosión.

        // Por ejemplo, puedes desactivar el objeto o destruirlo si es necesario.
        gameObject.SetActive(false);
    }








}
