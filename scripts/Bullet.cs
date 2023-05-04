using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BorderBullet"))
            Destroy(gameObject);
        
        if (collision.gameObject.CompareTag("Enemy"))
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.OnHit(dmg);
        Destroy(gameObject);
    }
    else if (collision.gameObject.CompareTag("BorderBullet"))
    {
        Destroy(gameObject);
    }
        
    } 
}
