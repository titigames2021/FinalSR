using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedUp")]
public class SpeedBuff : PowerUpEffect
{
 //Accede a la velocidad del player y la aumenta 
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().normalPlayerSpeed += amount;
    
    }






}
