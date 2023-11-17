using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 25;
    
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyRecieveDamage enemy = hitInfo.GetComponent<EnemyRecieveDamage>();
        if(enemy != null)
        {
            enemy.DealDamage(damage);
        }
        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}
