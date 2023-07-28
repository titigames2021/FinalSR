using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //Aqui le pasamos el scripteable object que hemos creado usando script como SpeedBuff y los parametros que queramos 
    public PowerUpEffect powerupEffect;

    //Aqui accedemos al metodo base para aplicar su metodo que permite aplicar los powerUps al player 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
            powerupEffect.Apply(collision.gameObject);
        }
        //Check player or enemy 
       
    }

}
