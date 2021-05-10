using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toasterController : MonoBehaviour
{
    public float health;
    public float missileDamage;
    public float volleyDamage;
    public float trackingDamage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
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
    }
}
