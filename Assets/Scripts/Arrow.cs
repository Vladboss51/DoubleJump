using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
