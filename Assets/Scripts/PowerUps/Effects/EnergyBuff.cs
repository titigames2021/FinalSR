using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/EnergyUp")]
public class EnergyBuff : PowerUpEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        if (target.GetComponent<PlayerController>().currentEnergy < 100.0f)
        {
            target.GetComponent<PlayerController>().currentEnergy += amount;
        }
       

    }
}
