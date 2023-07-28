using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float life=4.0f;
    public LayerMask playerproj;

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerproj == (playerproj | (1 << collision.gameObject.layer)))
        {
            life--;
        }
    }
}
