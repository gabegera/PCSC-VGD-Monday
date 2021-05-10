using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //Components
    private Rigidbody2D myRB;
    public GameObject missile;
    public GameObject volleyShot;
    public GameObject trackingMissile;

    //Bools
    public bool movementPressed;
    public bool isFacingRight;
    public bool missileEquipped;
    public bool volleyEquipped;
    public bool trackingMissileEquipped;

    //Vectors
    public Vector2 lookpos;
    private Quaternion zero;

    //Floats
    public float maxSpeed;
    public float speed;
    public float accelaration;
    public float decelaration;
    public float xRayDistance;
    public float yRayDistance;
    public float jumpSpeed;
    public float jumpCount;
    public float maxJumps;
    public float dashTimer;
    public float dashCooldown;
    public float dashSpeed;
    public float projSpeed;
    public float projAngle;
    public float projLifeSpan;
    public float missileCooldown;
    public float volleyCooldown;
    public float trackingMissileCooldown;
    public float missileFireRate;
    public float volleyFireRate;
    public float trackingMissileFireRate;
    public float volleyCount;
    public float volleyMaxCount;
    public float volleySpeed;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        missileEquipped = true;
        isFacingRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D groundRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), yRayDistance);
        RaycastHit2D rightRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), xRayDistance);
        RaycastHit2D leftRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), xRayDistance);

        //Movement with acceleration
        if ((Input.GetKey(KeyCode.A)) && (speed > 0))
        {
            isFacingRight = false;
            speed = speed - (decelaration * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.A)) && (speed > -maxSpeed))
        {
            isFacingRight = false;
            speed = speed - (accelaration * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.D)) && (speed < 0))
        {
            isFacingRight = true;
            speed = speed + (decelaration * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.D)) && (speed < maxSpeed))
        {
            isFacingRight = true;
            speed = speed + (accelaration * Time.deltaTime);
        }
        else
        {
            if (speed > (decelaration * Time.deltaTime))
            {
                speed = speed - (decelaration * Time.deltaTime);
            }
            else if (speed < (-decelaration * Time.deltaTime))
            {
                speed = speed + (decelaration * Time.deltaTime);
            }
            else
            {
                speed = 0;
            }
        }
        //

        //Detect when Movement Keys are Pressed
        if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
        {
            movementPressed = true;
        }
        else
        {
            movementPressed = false;
        }
        //
        
        //Jumping
        if ((Input.GetKeyDown(KeyCode.Space)) && (groundRay))
        {
            jumpCount = 0;
            Vector2 jumpVel = myRB.velocity;
            jumpVel.y = jumpSpeed;
            myRB.velocity = jumpVel;
        }
        else if ((Input.GetKeyDown(KeyCode.Space)) && (jumpCount < maxJumps))
        {
            jumpCount += 1;
            Vector2 jumpVel = myRB.velocity;
            jumpVel.y = jumpSpeed;
            myRB.velocity = jumpVel;
        }
        //

        //Wall Grabbing
        if ((rightRay || leftRay) && (groundRay == false))
        {
            myRB.gravityScale = 0;
            jumpCount = 0;
        }
        else
        {
            myRB.gravityScale = 1;
        }
        //


        //Wall Stopping
        if ((leftRay || rightRay) && (movementPressed == false))
        {
            speed = 0;
            
        }
        //

        //Dash Cooldown
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        //

        //Dashing
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (dashCooldown <= 0))
        {
            dashCooldown = dashTimer;
            if (isFacingRight == true)
            {
                speed = dashSpeed;
            }
            else if (isFacingRight == false)
            {
                speed = -dashSpeed;
            }

            
        }
        //

        //Shooting Cooldown
        if (missileCooldown > 0)
        {
            missileCooldown -= Time.deltaTime;
        }
        if (volleyCooldown > 0)
        {
            volleyCooldown -= Time.deltaTime;
        }
        if (trackingMissileCooldown > 0)
        {
            trackingMissileCooldown -= Time.deltaTime;
        }
        //

        //Selecting Weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            missileEquipped = true;
            volleyEquipped = false;
            trackingMissileEquipped = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            missileEquipped = false;
            volleyEquipped = true;
            trackingMissileEquipped = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            missileEquipped = false;
            volleyEquipped = false;
            trackingMissileEquipped = true;
        }

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if (missileEquipped == true && missileCooldown <= 0)
            {
                missileCooldown = missileFireRate;
                lookpos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                projAngle = Mathf.Atan2(lookpos.y, lookpos.x) * Mathf.Rad2Deg;
                GameObject b = Instantiate(missile, new Vector2(transform.position.x, transform.position.y), zero);
                Physics2D.IgnoreCollision(b.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());
                b.GetComponent<Rigidbody2D>().rotation = projAngle - 90;
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
                Destroy(b, projLifeSpan);
            }

            if (volleyEquipped == true && volleyCooldown <= 0)
            {
                volleyCooldown = volleyFireRate;
                StartCoroutine("volleyBurst");
            }

            if (trackingMissileEquipped == true && trackingMissileCooldown <= 0)
            {
                trackingMissileCooldown = trackingMissileFireRate;
                lookpos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                projAngle = Mathf.Atan2(lookpos.y, lookpos.x) * Mathf.Rad2Deg;
                GameObject b = Instantiate(trackingMissile, new Vector2(transform.position.x, transform.position.y), zero);
                Physics2D.IgnoreCollision(b.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());
                b.GetComponent<Rigidbody2D>().rotation = projAngle - 90;
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
            }



        }

        if (volleyCount >= volleyMaxCount)
        {
            volleyCount = 0;
        }
        //

        //Updates X Velocity
        Vector2 velocity;
        velocity = myRB.velocity;
        velocity.x = speed;
        myRB.velocity = velocity;
        //
    }

    IEnumerator volleyBurst()
    {
        while (volleyCount < volleyMaxCount)
        {
            yield return new WaitForSeconds(volleySpeed);
            volleyCount += 1;
            lookpos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            projAngle = Mathf.Atan2(lookpos.y, lookpos.x) * Mathf.Rad2Deg;
            GameObject b = Instantiate(volleyShot, new Vector2(transform.position.x, transform.position.y), zero);
            Physics2D.IgnoreCollision(b.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
        }

    }

}
