using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    private Component myRB;
    private Animator myAnim;
    public GameObject bullet;
    public GameObject player;

    private Quaternion zero;
    private Vector2 playerPos;

    public bool canShoot;

    public float health;
    public float projSpeed;
    public float fireRate;
    public float burstCount;
    public float burstMax;
    public float burstCooldown;
    private float startingBurstCooldown;
    public float projAngle;
    public float bulletLifeSpan;

    public float volleyDamage;
    public float trackingDamage;
    public float missileDamage;


    // Start is called before the first frame update
    void Start()
    {
        startingBurstCooldown = burstCooldown;
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        
        if (burstCount == burstMax)
        {
            StopCoroutine("burst");
            burstCooldown = startingBurstCooldown;
            burstCount = 0;
        }

        if (burstCooldown > 0)
        {
            burstCooldown -= Time.deltaTime;
        }
        else
        {
            burstCooldown = 0;
        }

        if (burstCooldown <= 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        if (canShoot)
        {
            myAnim.SetBool("isAttacking", true);
            StartCoroutine("burst");
        }
        else
        {
            myAnim.SetBool("isAttacking", false);
        }

    }

    IEnumerator burst()
    {
        while (burstCount < burstMax)
        {
            yield return new WaitForSeconds(fireRate);
            burstCount += 1;
            projAngle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), zero);
            Physics2D.IgnoreCollision(b.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
            b.GetComponent<Rigidbody2D>().rotation = projAngle - 90;
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(projSpeed * Mathf.Cos(projAngle * Mathf.Deg2Rad), projSpeed * Mathf.Sin(projAngle * Mathf.Deg2Rad));
            Destroy(b, bulletLifeSpan);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("volley"))
        {
            health -= volleyDamage;
        }
        else if (collision.gameObject.name.Contains("tracking"))
        {
            health -= trackingDamage;
        }
        else if (collision.gameObject.name.Contains("missile"))
        {
            health -= missileDamage;
        }
    }

}
