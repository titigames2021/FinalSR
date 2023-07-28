using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class B02Projectile : MonoBehaviour
{
    public float speed;
    public Transform playerPos;
    private float currentTime;
    public float lifeTime;
    private Rigidbody2D objRb;
    public GameObject[] powerUps;
    public GameObject outside;
    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
        FindOutsidePoint();
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
        float step = speed * Time.deltaTime;


        Vector3 currentPosition = transform.position;
        Vector3 direction = playerPos.position - transform.position;

        // Calcula el ángulo en grados entre la dirección y el eje hacia arriba (Vector3.up)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica la rotación en el eje Z para mirar hacia el jugador
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = Vector3.MoveTowards(currentPosition, playerPos.position, step);
        
        if(playerPos.position.x>= transform.position.x)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
       
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerPos = player.transform;
    }

    private void FindOutsidePoint()
    {
        outside = GameObject.FindWithTag("OP");
        
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
