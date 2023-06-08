// Ce script permet de gérer les enemies.
// Auteur: Jabote Mohamed Amine.
// Date: 2023-04-01.
// Dans le cadre de l'UE GL01 à l'UTT.
// Semestre : P23.
// Projet : Projet de jeu vidéo "Behe Goat".
//langage: C#.

//Appel des bibliothèques
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Début 
public class Enemy : MonoBehaviour
{

    //Déclaration des variables
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


    //Initialisation
   void Awake() {
        spriteRender = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.left * speed;
   }


    //OnHit: Fonction qui permet de gérer les dégats infligés à l'ennemi
   public void OnHit(int dmg)
   {
    print("Hit!");
    health -= dmg;
    Invoke("ReturnSprite", 0.1f);

    if(health <= 0)
    {
        print("Enemy Die!");
        Ctrl_Player playerLogic = player.GetComponent<Ctrl_Player>();
        Destroy(gameObject);
    }
   }


   void OnTriggerEnter2D(Collider2D collision)
   {
    //Si l'objet "Bullet" entre en collision avec un objet "Enemy" alors on détruit l'objet "Bullet" et on inflige des dégats à l'objet "Enemy"
    if(collision.gameObject.CompareTag("BorderBullet"))
        Destroy(gameObject);

    //Sinon si l'objet "PlayerBullet" entre en collision avec un objet "Enemy" alors on détruit l'objet "PlayerBullet" et on inflige des dégats à l'objet "Enemy"
    else if (collision.gameObject.CompareTag("PlayerBullet")){
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null) // Vérification si le GameObject possède bien le composant Bullet
        {
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }
    }
    else if (collision.gameObject.CompareTag("Player"))//Sino si l'objet "Player" entre en collision avec un objet "Enemy" alors on détruit l'objet "Player"
    {
        collision.gameObject.SetActive(false);
   }
}

//Fin
}