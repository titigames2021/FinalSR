using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/ProjectailSpeed")]
public class ProjectailSpeedBuff : PowerUpEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().normalSpeedProjectail += amount;

    }
}
