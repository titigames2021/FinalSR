using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_End : MonoBehaviour
{
    public Transform playerShip;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
       
        GameObject player = GameObject.FindWithTag("Player");
        playerShip = player.transform;
        transform.position = new Vector3(playerShip.position.x + 10.0f, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
