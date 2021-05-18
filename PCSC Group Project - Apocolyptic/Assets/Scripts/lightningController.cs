using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningController : MonoBehaviour
{
    public float lifespan;
    public float maxLife;


    // Start is called before the first frame update
    void Start()
    {
        lifespan = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifespan -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            collision.gameObject.GetComponent<playerController>().health -= 40;
        }
    }
}
