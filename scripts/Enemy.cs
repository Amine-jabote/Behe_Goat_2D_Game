using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   public float speed;
   public int health; 
   public Sprite[] sprites;
   public string enemyName;
   
   public GameObject bulletObjA;
   public GameObject bulletObjB;
   public GameObject player;
   public float power;
   public float maxShotDelay;
   public float curShotDelay;


   SpriteRenderer spriteRender;
   Rigidbody2D rigid;

   public Level1Manager manager;

   void Awake() {
        spriteRender = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.left * speed;
   }

   void Fire()
   {
    if (curShotDelay < maxShotDelay)
        return;
    if (enemyName == "S")
    {
        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector3 dirVec = player.transform.position - transform.position;
        rigid.AddForce(dirVec * 10, ForceMode2D.Impulse);
    }
    else if (enemyName == "L")
    {
        GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
        GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

        Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
        Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

        rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
        rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);
    }

    curShotDelay = 0;
   }
   void OnHit(int dmg)
   {
    health -= dmg;
    spriteRender.sprite = sprites[0];
    Invoke("ReturnSprite", 0.1f);

    if(health <= 0)
    {
        Ctrl_Player playerLogic = player.GetComponent<Ctrl_Player>();
        Destroy(gameObject);
    }
   }

   void ReturnSprite()
   {
    spriteRender.sprite = sprites[0];
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
    if(collision.gameObject.CompareTag("BorderBullet"))
        Destroy(gameObject);
    else if (collision.gameObject.CompareTag("PlayerBullet")){
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        OnHit(bullet.dmg);
        Destroy(collision.gameObject);
    }
    else if (collision.gameObject.CompareTag("Player"))
    {
        collision.gameObject.SetActive(false);
   }
}
}
