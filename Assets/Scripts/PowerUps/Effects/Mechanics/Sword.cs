using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/Mechanics/Sword")]
public class Sword : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().swordTrigger = true;
        target.GetComponent<PlayerController>().currentEnergy = 100.0f;
        target.GetComponent<PlayerController>().swordEnabled = true;
        target.GetComponent<PlayerController>().usingMejora = false;


    }
}
