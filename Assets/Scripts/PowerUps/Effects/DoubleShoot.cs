using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/DoubleShoot")]
public class DoubleShoot : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().dobleShoot=true;

    }
}
