using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_3 : EnemyBase
{
    
    private GameObject obj2;
    private GameObject obj3;
    private GameObject obj4;
    public Transform throwpoint1;
    public Transform throwpoint2;
    public Transform throwpoint3;
    public Transform throwpoint4;

    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
        FindPool();
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
            //Movement
            transform.Rotate(Vector3.forward, 20.0f * speed * Time.deltaTime);

        }
      
    }

    private void LateUpdate()
    {

        //Activation
        if (canShoot & isActive)
        {
            SpinShoot();
        }

    }

    private void SpinShoot()
    {
        canShoot = false;

        obj = enemiePool.GetPooledObject();
        obj2 = enemiePool.GetPooledObject();
        obj3 = enemiePool.GetPooledObject();
        obj4 = enemiePool.GetPooledObject();
        if (obj == null) return;

        obj.transform.position = throwpoint1.position;
        obj.transform.rotation = throwpoint1.rotation;
        obj.SetActive(true);

        obj2.transform.position = throwpoint2.position;
        obj2.transform.rotation = throwpoint2.rotation;
        obj2.SetActive(true);

        obj3.transform.position = throwpoint3.position;
        obj3.transform.rotation = throwpoint3.rotation;
        obj3.SetActive(true);

        obj4.transform.position = throwpoint4.position;
        obj4.transform.rotation = throwpoint4.rotation;
        obj4.SetActive(true);

        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
        objRb.AddForce(-obj.gameObject.transform.right * projectailSpeed, ForceMode2D.Impulse);
        
                Rigidbody2D objRb2 = obj2.GetComponent<Rigidbody2D>();
                objRb2.AddForce(-obj2.gameObject.transform.right * projectailSpeed, ForceMode2D.Impulse);

                Rigidbody2D objRb3 = obj3.GetComponent<Rigidbody2D>();
                objRb3.AddForce(-obj3.gameObject.transform.right * projectailSpeed, ForceMode2D.Impulse);

                Rigidbody2D objRb4 = obj4.GetComponent<Rigidbody2D>();
                objRb4.AddForce(-obj4.gameObject.transform.right * projectailSpeed, ForceMode2D.Impulse);
        Invoke("ResetCooldown", coolDownTime);

    }

    private void ResetCooldown()
    {
        // Reiniciar el indicador de si se puede lanzar un objeto
        canShoot = true;

    }

}
