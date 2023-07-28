using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public bool open;
    public Transform door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open && door.localScale.y>=0){

            door.localScale -= Vector3.up * Time.deltaTime;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.tag=="doorbutton" && collision.collider.gameObject.layer==3)
        {


            open = true;




        }
    }
}
