using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/Mechanics/WitchTimeTrigger")]
public class WitchTimeTrigger : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
        Debug.Log("Al ataaaaqueeerlll");
        target.GetComponent<PlayerController>().witchTimeTrigger = true;
        target.GetComponent<PlayerController>().currentEnergy = 100.0f;
        target.GetComponent<PlayerController>().runWT = true;
       
       

    }
}
