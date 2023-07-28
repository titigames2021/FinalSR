using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_4 : EnemyBase
{

    public Transform point;

    public float rSpeed;
    public float radio;
    private float xMovement;




    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        ActiveEnemy();
        DistacePlayer();

        if (isActive)
        {
            CheckLife();
           
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            xMovement += Time.deltaTime * speed;
            float angle = Time.time * rSpeed;
            // Calcula las coordenadas X, Y, Z utilizando funciones trigonométricas
            float x = Mathf.Sin(angle) * radio - xMovement;
            float z = 0f; // Mantiene una órbita en el mismo plano horizontal
            float y = Mathf.Cos(angle) * radio;

            // Establece la posición relativa al objeto central
            transform.position = point.position + new Vector3(x, y, z);


        }
    }
}
