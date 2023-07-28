using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{

    private GameObject obj;
    public Transform playerPos;
    public ObjectPoolerScript rocksPool;

    public float coolDownTime;
  
    public Vector3 spawnRock;
    public float disableDistance;
    public float activeDistance;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FallRock", 1.0f, 1.0f);

    }

    // Update is called once per frame
   




    protected void FallRock()
    {
        
        //float variation = Random.Range(-2, 5);
        //spawnRock.x = spawnRock.x + variation;

        if (playerPos.position.x <= disableDistance && playerPos.position.x >= activeDistance)
        {
            obj = rocksPool.GetPooledObject();
            if (obj == null) return;
            Vector2 rockpos = playerPos.transform.position + spawnRock;
            obj.transform.position = rockpos;
       
            obj.SetActive(true);
          
        }
        else
        {
            gameObject.SetActive(false);
        }





    }

  
}
