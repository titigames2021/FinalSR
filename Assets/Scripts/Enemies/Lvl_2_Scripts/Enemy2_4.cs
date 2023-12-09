using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_4 : EnemyBase
{
    //public GameObject[] projectile;
    private GameObject obj2;
    private GameObject obj3;
    private GameObject obj4;
    private GameObject obj5;
    private GameObject obj6;

    public Transform[] thr;

   
    public Rigidbody2D[] objRb;


    int projectileCount = 6; // Número total de elementos en el array
    private int shootCounter;
    private int maxShootCount=2;

    void Start()
    {
        FindPlayer();
        FindPool();
        canShoot = true;
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
            //Movement
            transform.Rotate(Vector3.forward, 20.0f * speed * Time.deltaTime);

        }

    }

    private void LateUpdate()
    {

        //Activation
        if (canShoot & isActive)
        {
            InvokeRepeating("Shoot6", 1.0f, 2.0f);
        }

    }

    private void Shoot6()
    {
        if (shootCounter >= maxShootCount)
        {
            // Cancelar la repetición de la llamada a Shoot6()
            CancelInvoke();
            // Reiniciar el contador
            shootCounter = 0;
            return;
        }
        

        int projectileCount = 6; // Número total de elementos en el array
        GameObject[] projectile = new GameObject[projectileCount]; // Array de GameObjects

        for (int i = 0; i < projectileCount; i++)
        {
            projectile[i] = enemiePool.GetPooledObject();
            if (projectile[i] != null)
            {
                projectile[i].transform.position = thr[i].position;
                projectile[i].transform.rotation = thr[i].rotation;
                projectile[i].SetActive(true);
                Rigidbody2D objRb = projectile[i].GetComponent<Rigidbody2D>();
                objRb.AddForce(-projectile[i].transform.right * projectailSpeed, ForceMode2D.Impulse);

            }
            else
            {
                Debug.LogWarning("No se pudo obtener un objeto de la pool en el índice " + i);
            }
        }


        shootCounter++;

        
    }

    private void ResetCooldown()
    {
        // Reiniciar el indicador de si se puede lanzar un objeto
        canShoot = true;

    }
}
