using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/TripleShoot")]
public class TripleShoot : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().tripleShoot = true;

    }
}
