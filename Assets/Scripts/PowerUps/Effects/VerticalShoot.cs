using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/VerticalShoot")]
public class VerticalShoot : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().verticalShoot = true;

    }
}
