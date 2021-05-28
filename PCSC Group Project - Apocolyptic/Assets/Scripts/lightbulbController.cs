using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightbulbController : MonoBehaviour
{
    public playerController playerController;
    public GameObject player;
    //public GameObject attack;
    private Rigidbody2D RB;

    private Quaternion zero;

    public bool playerDetected;

    public float health;
    public float missileDamage;
    public float volleyDamage;
    public float trackingDamage;
    public float maxSpeed;
    public float speed;
    public float accelaration;
    public float deceleration;
    public float playerDirection;
    public float detectionCooldown;
    public float detectionTimer;
    public float xRayDistance;
    public float yRayDistance;
    public float knockback;
    public float attackCooldown;
    public float attackTimer;


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        playerController = GameObject.Find("player").GetComponent<playerController>();
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D groundRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), yRayDistance);
        RaycastHit2D rightRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), xRayDistance);
        RaycastHit2D leftRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), xRayDistance);

        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }

        playerDirection = transform.position.x - player.transform.position.x;

        if (detectionCooldown >= 0)
        {
            detectionCooldown -= Time.deltaTime;
        }

        if (playerDetected == true && detectionCooldown <= 0)
        {
            if (playerDirection > 1.5)
            {
                Vector2 velocity;
                velocity = RB.velocity;
                velocity.x = -speed;
                RB.velocity = velocity;
            }
            else if (playerDirection < -1.5)
            {
                Vector2 velocity;
                velocity = RB.velocity;
                velocity.x = speed;
                RB.velocity = velocity;
            }
            else
            {
                Vector2 velocity;
                velocity = RB.velocity;
                velocity.x = 0;
                RB.velocity = velocity;
            }
        }

        if (leftRay.collider.gameObject.Equals(player))
        {
            if (attackCooldown <= 0)
            {
                playerController.health -= 1;
                attackCooldown = attackTimer;
            }

        }
        else if (rightRay.collider.gameObject.Equals(player))
        {
            if (attackCooldown <= 0)
            {
                playerController.health -= 1;
                attackCooldown = attackTimer;
            }
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("missile"))
        {
            //GameObject dP = Instantiate(deathParticles, new Vector2(transform.position.x, transform.position.y), zero);
            //Destroy(dP, 3f);
            health -= missileDamage;
        }

        if (collision.gameObject.name.Contains("volley"))
        {
            //GameObject dP = Instantiate(deathParticles, new Vector2(transform.position.x, transform.position.y), zero);
            //Destroy(dP, 3f);
            health -= volleyDamage;
        }

        if (collision.gameObject.name.Contains("tracking"))
        {
            //GameObject dP = Instantiate(deathParticles, new Vector2(transform.position.x, transform.position.y), zero);
            //Destroy(dP, 3f);
            health -= trackingDamage;
        }

        //if (collision.gameObject.name.Contains("player"))
        //{
        //    if (playerDirection > 0)
        //    {
        //        //RB.AddForce(collision.relativeVelocity * (knockback * 100), ForceMode2D.Force);
        //        playerDetected = false;
        //        detectionCooldown = detectionTimer;
        //        Vector2 velocity;
        //        velocity = RB.velocity;
        //        velocity.x = knockback;
        //        velocity.y = knockback;
        //        RB.velocity = velocity;
        //    }
        //    else if (playerDirection < 0)
        //    {
        //        //RB.AddForce(collision.relativeVelocity * (knockback * 100), ForceMode2D.Force);
        //        playerDetected = false;
        //        detectionCooldown = detectionTimer;
        //        Vector2 velocity;
        //        velocity = RB.velocity;
        //        velocity.x = -knockback;
        //        velocity.y = knockback;
        //        RB.velocity = velocity;
        //    }
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            playerDetected = false;
        }
    }
}
