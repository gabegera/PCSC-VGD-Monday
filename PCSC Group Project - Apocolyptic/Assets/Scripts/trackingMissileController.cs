using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingMissileController : MonoBehaviour
{
    //Vectors
    public Vector2 lookpos;
    private Quaternion zero;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("toaster"))
        {
            
        }
    }
}
