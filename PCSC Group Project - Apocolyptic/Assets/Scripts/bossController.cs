using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public GameObject circleProj;
    private Quaternion zero;

    public float projSpeed;
    public float projTimer;
    public float fireRate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projTimer < 1)
        {
            projTimer = projTimer + (fireRate * Time.deltaTime);
        }
        else if (projTimer > 1)
        {
            projTimer = 0;

            GameObject b1 = Instantiate(circleProj, new Vector2(transform.position.x, transform.position.y), zero);
            b1.GetComponent<Rigidbody2D>().velocity = b1.transform.forward * projSpeed;
            b1.GetComponent<Rigidbody2D>().rotation = 30;
            Physics2D.IgnoreCollision(b1.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }

    }
}
