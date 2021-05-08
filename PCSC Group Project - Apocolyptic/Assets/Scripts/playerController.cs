using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //Components
    private Rigidbody2D myRB;

    //Bools
    public bool movementPressed;
    public bool isFacingRight;

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


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
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



        Vector2 velocity;
        velocity = myRB.velocity;
        velocity.x = speed;
        myRB.velocity = velocity;
        
    }
}
