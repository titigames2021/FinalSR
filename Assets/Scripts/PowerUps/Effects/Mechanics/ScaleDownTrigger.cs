using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Mechanics/ScaleDown")]
public class ScaleDownTrigger : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().scaleDownTrigger = true;
        target.GetComponent<PlayerController>().currentEnergy = 100.0f ;
        target.GetComponent<PlayerController>().scalingDown = true;
       
    }
}
