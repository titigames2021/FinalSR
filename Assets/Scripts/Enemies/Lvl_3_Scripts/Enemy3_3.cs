using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_3 : EnemyBase
{
    public Transform gun;
    public Transform thr2;
    public Transform thr3;
    private Transform thr1;
    public Transform upPoint;
    public Transform downPoint;
    public delegate void  toWaterEventHandler();
    public event toWaterEventHandler waterEvent;
    public Transform limitx;
    private float startspeed;
    public GameObject sword;
  
    private void Start()
    {
        FindPool();
        FindPlayer();
        FindPlayerShip();
        canShoot = true;
        thr1 = throwpoint;
        startspeed = speed;
    }


    // Update is called once per frame
    void Update()
    {
       
        Vector3 direction = playerShip.position - gun.transform.position;
        // Calcula el ángulo en grados entre la dirección y el eje hacia arriba (Vector3.left)
        float angle = (Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg)-13.0f;
      
        
        angle = Mathf.Clamp(angle,   15.0f, 120.0f);

        if (playerShip.position.y <= downPoint.position.y)
        {
            throwpoint = thr2;
        }
        else if(playerShip.position.y >= upPoint.position.y)
        {
            throwpoint = thr1;
        }
        else
        {
            throwpoint = thr3;
        }

       

        // Aplica la rotación en el eje Z para mirar hacia el jugador
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //Life
        CheckLife();
        //Activation
        DisableEnemy();
        DistacePlayer();
        ActiveEnemy();

        //Mov
        if (isActive)
        {


            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
            
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

    private void OnDisable()
    {
        Instantiate(sword, gameObject.transform.position, Quaternion.identity);
        if (waterEvent != null)
        {
            waterEvent();
        }
    }



}
