using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Transform playerT;
    private float deleteDistance = -15.0f;
    private float playerD;
    // Start is called before the first frame update
  

    public void Delete()
    {
        playerD = gameObject.transform.position.x - playerT.position.x;
        if (playerD <= deleteDistance)
        {
            gameObject.SetActive(false);
        }
    }


}
