using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{

    // metodo base de todos los power ups 
    public abstract void Apply(GameObject target);



}
