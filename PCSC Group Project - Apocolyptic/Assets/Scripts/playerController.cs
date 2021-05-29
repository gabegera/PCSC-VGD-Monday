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
    public Animator myAnim;

    //Bools
    public bool movementPressed;
    public bool wallGrabbing;
    public bool isFacingRight;
    public bool missileEquipped;
    public bool volleyEquipped;
    public bool trackingMissileEquipped;
    public bool isDashing;

    //Vectors
    public Vector2 lookpos;
    public Vector2 feet;
    private Quaternion zero;

    //Floats
    public float health;
    public float maxSpeed;
    public float speed;
    public float accelaration;
    public float decelaration;
    public float xRayDistance;
    public float yRayDistance;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float jumpCount;
    public float maxJumps;
    public float wallJumpSpeed;
    public float dashTimer;
    public float dashCooldown;
    public float dashInvin;
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
    public float fallMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        missileEquipped = true;
        isFacingRight = true;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }

        feet = transform.position;
        feet.y -= 0.8f;

        RaycastHit2D groundRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), yRayDistance);
        RaycastHit2D rightRay = Physics2D.Raycast(feet, transform.TransformDirection(Vector2.right), xRayDistance);
        RaycastHit2D leftRay = Physics2D.Raycast(feet, transform.TransformDirection(Vector2.left), xRayDistance);


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

        //Stops movement when running into a wall
        if (Input.GetKey(KeyCode.A) && leftRay && isFacingRight == false)
        {
            speed = 0;
        }
        else if (Input.GetKey(KeyCode.D) && rightRay && isFacingRight == true)
        {
            speed = 0;
        }
        //

        //Makes the falling Speed Faster
        if ((myRB.velocity.y <= 0) && (wallGrabbing == false))
        {
            myRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
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
            jumpVel.y = doubleJumpSpeed;
            myRB.velocity = jumpVel;
        }
        //

        //Wall Grabbing
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (rightRay || leftRay) && (groundRay == false))
        {
            Vector2 jumpVel = myRB.velocity;
            jumpVel.y = 0;
            myRB.velocity = jumpVel;
            jumpCount = 0;
        }

        if ((Input.GetKey(KeyCode.LeftShift)) && (rightRay || leftRay) && (groundRay == false))
        {
            myRB.gravityScale = 0;
            speed = 0;
            wallGrabbing = true;
        }
        else
        {
            wallGrabbing = false;
            myRB.gravityScale = 1;
        }
        //

        //Wall Jumping
        if (wallGrabbing && Input.GetKey(KeyCode.Space))
        {
            if (leftRay)
            {
                speed = wallJumpSpeed;
            }
            else if (rightRay)
            {
                speed = -wallJumpSpeed;
            }
        }
        //

        //Dash Cooldown
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        if (dashCooldown <= (dashTimer - dashInvin))
        {
            isDashing = false;
        }
        //

        //Dashing
        if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashCooldown <= 0))
        {
            dashCooldown = dashTimer;
            isDashing = true;
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

        //Animations
        if (groundRay == false && isFacingRight == true)
        {
            myAnim.SetBool("notMoving", false);
            myAnim.SetBool("notMovingLeft", false);
            myAnim.SetBool("isWalkingLeft", false);
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunningLeft", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isJumping", true);
            myAnim.SetBool("isJumpingLeft", false);
        }
        else if (groundRay == false && isFacingRight == false)
        {
            myAnim.SetBool("notMoving", false);
            myAnim.SetBool("notMovingLeft", false);
            myAnim.SetBool("isWalkingLeft", false);
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunningLeft", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isJumpingLeft", true);
        }
        else if (speed > 0 && speed < 5 && groundRay)
        {
            myAnim.SetBool("notMoving", false);
            myAnim.SetBool("notMovingLeft", false);
            myAnim.SetBool("isWalkingLeft", false);
            myAnim.SetBool("isWalking", true);
            myAnim.SetBool("isRunningLeft", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isJumpingLeft", false);
        }
        else if (speed < 0 && speed > -5 && groundRay)
        {
            myAnim.SetBool("notMoving", false);
            myAnim.SetBool("notMovingLeft", false);
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isWalkingLeft", true);
            myAnim.SetBool("isRunningLeft", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isJumpingLeft", false);
        }
        else if (speed > 5 && groundRay)
        {
            myAnim.SetBool("notMoving", false);
            myAnim.SetBool("notMovingLeft", false);
            myAnim.SetBool("isWalkingLeft", false);
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunningLeft", false);
            myAnim.SetBool("isRunning", true);
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isJumpingLeft", false);
        }
        else if (speed < -5 && groundRay)
        {
            myAnim.SetBool("notMoving", false);
            myAnim.SetBool("notMovingLeft", false);
            myAnim.SetBool("isWalkingLeft", false);
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunningLeft", true);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isJumpingLeft", false);
        }
        else if (isFacingRight && speed == 0 && groundRay)
        {
            myAnim.SetBool("isWalkingLeft", false);
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("notMovingLeft", false);
            myAnim.SetBool("notMoving", true);
            myAnim.SetBool("isRunningLeft", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isJumpingLeft", false);
        }
        else if (isFacingRight == false && speed == 0 && groundRay)
        {
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isWalkingLeft", false);
            myAnim.SetBool("notMoving", false);
            myAnim.SetBool("notMovingLeft", true);
            myAnim.SetBool("isRunningLeft", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isJumpingLeft", false);
        }
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
            Physics2D.IgnoreCollision(b.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
            b.GetComponent<Rigidbody2D>().rotation = projAngle - 90;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing == false)
        {
            if (collision.gameObject.name.Contains("toaster"))
            {
                health -= 1;
            }
            else if (collision.gameObject.name.Contains("bossBullet"))
            {
                health -= 1;
            }
            else if (collision.gameObject.name.Contains("Ball"))
            {
                health -= 1;
            }
        }

        if (collision.gameObject.name.Contains("spike"))
        {
            health = 0;
        }

    }

}
