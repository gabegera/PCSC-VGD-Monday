using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volleyController : MonoBehaviour
{
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
        if (collision.gameObject.name.Contains("bossBullet"))
        {
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), collision.gameObject.GetComponent<PolygonCollider2D>());
        }
        else
        {
            Destroy(gameObject);
        }

            
    }
}
