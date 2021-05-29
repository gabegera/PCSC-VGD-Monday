using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private SpriteRenderer mySprite;
    private Animator myAnim;
    public GameObject bullet;
    public GameObject ball;
    public GameObject player;

    private Quaternion zero;
    public Transform bulletOrigin;
    public Vector2 playerPos;

    public bool canShoot;
    public bool isHit;

    public float attackPhase = 1;
    public float health;
    private float startingHealth;
    public float projSpeed;
    public float fireRate;
    public float circleCount;
    public float circleMax;
    public float circleCooldown;
    private float startingCircleCooldown;
    public float burstCount;
    public float burstMax;
    public float burstCooldown;
    private float startingBurstCooldown;
    public float ballCooldown;
    private float startingBallCooldown;
    public float projAngle;
    public float circleAngle;
    public float startingCircleAngle;
    private float startingAngle;
    public float angleAddition;
    public float bulletLifeSpan;


    public float volleyDamage;
    public float trackingDamage;
    public float missileDamage;

    public float colorChange;
    private float colorChangeStart;


    // Start is called before the first frame update
    void Start()
    {
        startingBurstCooldown = burstCooldown;
        startingCircleCooldown = circleCooldown;
        startingBallCooldown = ballCooldown;
        colorChangeStart = colorChange;
        startingHealth = health;
        mySprite = GetComponent<SpriteRenderer>();
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        player = GameObject.Find("player");
        startingAngle = projAngle;
        startingCircleAngle = circleAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("EndMenu");
        }


        if (isHit)
        {
            colorChange -= Time.deltaTime;
        }

        if (colorChange <= 0)
        {
            mySprite.color = Color.white;
            isHit = false;
            colorChange = colorChangeStart;
        }

        if ((health <= (startingHealth * 2)/3) && (health > (startingHealth / 3)))
        {
            attackPhase = 2;
        }
        else if ((health > 0) && (health < (startingHealth / 3)))
        {
            attackPhase = 3;
        }

        if (attackPhase == 1)
        {
            if (circleCount >= circleMax)
            {
                circleCooldown = startingCircleCooldown;
                circleCount = 0;
                canShoot = false;
            }

            if (circleCooldown > 0)
            {
                circleCooldown -= Time.deltaTime;
            }
            else if (circleCooldown <= 0)
            {
                myAnim.SetBool("isAttacking", true);
                canShoot = true;
            }

            if (canShoot)
            {
                while ((circleCount < circleMax))
                {
                    playerPos = player.GetComponent<Rigidbody2D>().transform.position - bulletOrigin.transform.position;
                    circleAngle = circleAngle * (circleCount + 1);
                    //projAngle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
                    GameObject b = Instantiate(bullet, new Vector2(bulletOrigin.transform.position.x, bulletOrigin.transform.position.y), zero);
                    Physics2D.IgnoreCollision(b.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
                    b.GetComponent<Rigidbody2D>().rotation = circleAngle - 90;
                    b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(circleAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(circleAngle * Mathf.Deg2Rad));
                    circleCount += 1;
                    circleAngle = Random.Range(startingCircleAngle, (startingCircleAngle + 15));
                
                }


            }
            else
            {
                myAnim.SetBool("isAttacking", false);
            
            }
        }
        else if (attackPhase == 2)
        {
            if (burstCooldown > 0)
            {
                burstCooldown -= Time.deltaTime;
            }
            else if (burstCooldown <= 0)
            {
                myAnim.SetBool("isAttacking", true);
                canShoot = true;
                burstCooldown = startingBurstCooldown;
            }

            if (canShoot)
            {
                while ((canShoot))
                {
                    playerPos = player.GetComponent<Rigidbody2D>().transform.position - bulletOrigin.transform.position;
                    projAngle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
                    GameObject b = Instantiate(bullet, new Vector2(bulletOrigin.transform.position.x, bulletOrigin.transform.position.y), zero);
                    Physics2D.IgnoreCollision(b.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
                    b.GetComponent<Rigidbody2D>().rotation = projAngle - 90;
                    b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
                    canShoot = false;
                    //burstCount += 1;

                }


            }
            else
            {
                myAnim.SetBool("isAttacking", false);

            }
        }
        else if (attackPhase == 3)
        {
            if (ballCooldown > 0)
            {
                ballCooldown -= Time.deltaTime;
            }
            else if (ballCooldown <= 0)
            {
                myAnim.SetBool("isAttacking", true);
                canShoot = true;
                ballCooldown = startingBallCooldown;
            }

            if (canShoot)
            {
                while ((canShoot))
                {
                    playerPos = player.GetComponent<Rigidbody2D>().transform.position - bulletOrigin.transform.position;
                    projAngle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
                    GameObject b = Instantiate(ball, new Vector2(bulletOrigin.transform.position.x, bulletOrigin.transform.position.y), zero);
                    Physics2D.IgnoreCollision(b.GetComponent<CircleCollider2D>(), GetComponent<PolygonCollider2D>());
                    b.GetComponent<Rigidbody2D>().rotation = projAngle - 90;
                    b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
                    canShoot = false;
                    //burstCount += 1;

                }


            }
            else
            {
                myAnim.SetBool("isAttacking", false);

            }
        }





    }

    //IEnumerator burst()
    //{
    //    while (burstCount < burstMax)
    //    {
    //        yield return new WaitForSeconds(fireRate);
    //        burstCount += 1;
    //        playerPos = player.GetComponent<Rigidbody2D>().transform.position - bulletOrigin.transform.position;
    //        Mathf.Clamp(projAngle, minAngle, maxAngle);
    //        projAngle = projAngle * (burstCount + 1);

    //        //projAngle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
    //        GameObject b = Instantiate(bullet, new Vector2(bulletOrigin.transform.position.x, bulletOrigin.transform.position.y), zero);
    //        Physics2D.IgnoreCollision(b.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
    //        b.GetComponent<Rigidbody2D>().rotation = projAngle - 90;
    //        b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("volley"))
        {
            health -= volleyDamage;

            mySprite.color = Color.red;
            isHit = true;
        }
        else if (collision.gameObject.name.Contains("tracking"))
        {
            health -= trackingDamage;

            mySprite.color = Color.red;
            isHit = true;
        }
        else if (collision.gameObject.name.Contains("missile"))
        {
            health -= missileDamage;

            mySprite.color = Color.red;
            isHit = true;
        }
    }

}
