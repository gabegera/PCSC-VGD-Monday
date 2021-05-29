using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingMissileController : MonoBehaviour
{
    //Components
    private Rigidbody2D myRB;

    //Game Objects
    public GameObject trackedObject;
    public playerController player;

    //Vectors
    public Vector2 enemyPos;
    
    private Quaternion zero;

    //Floats
    private float projAngle;

    //Bools
    public bool foundEnemy;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<playerController>();
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (foundEnemy == true)
        {
            enemyPos = trackedObject.transform.position - transform.position;
            projAngle = Mathf.Atan2(enemyPos.y, enemyPos.x) * Mathf.Rad2Deg;
            myRB.velocity = new Vector2(player.projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), player.projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
            myRB.rotation = projAngle - 90;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name.Contains("Enemy") || collision.gameObject.name.Contains("Boss")) && foundEnemy == false)
        {
            foundEnemy = true;
            trackedObject = collision.gameObject;
        }
    }
}
