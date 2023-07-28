using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{

    private Rigidbody2D objRb;
    public float lifeTime;
    private float currentTime;
    public GameObject[] powerUps;
    // Start is called before the first frame update
    void Start()
    {
       
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 12);

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= lifeTime)
        {

            Reset();

        }

    }
    private void Reset()
    {
        currentTime = 0.0f;
        objRb = gameObject.GetComponent<Rigidbody2D>();
        objRb.velocity = Vector2.zero;
        gameObject.SetActive(false);
        int randomNum = UnityEngine.Random.Range(0, powerUps.Length);
        Instantiate(powerUps[randomNum], transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        
            Invoke("Reset", 0.0f);

    }

 

}
