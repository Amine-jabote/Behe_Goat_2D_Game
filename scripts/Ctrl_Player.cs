// Ce script permet de gérer le personnage principale du jeu.
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
public class Ctrl_Player : MonoBehaviour
{

    //Déclaration des variables
    public int life;

    public float speed;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public float power;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    

    public Level1Manager manager;


    void Start()
    {
        manager = GameObject.Find("LevelManager").GetComponent<Level1Manager>();
    }


    //Mis à jour
    void Update()
    {
        //Si le joueur touche le haut de l'écran, il ne peut pas aller plus haut
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchTop && h == 1) || (isTouchBottom && h == -1)){
            h = 0;
        }
        //Si le joueur touche le bas de l'écran, il ne peut pas aller plus bas
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchRight && v == -1) || (isTouchLeft && v == 1)){
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

       
       //Si le joueur touche le key "espace" dans son clavier le joueur tire
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Fire();
    }
    //Modidication du temps nécessaire pour tirer
    curShotDelay += Time.deltaTime;

        
    }

    void Fire()
    {
        print("Fire");
        //Si le joueur n'a pas attendu le temps nécessaire pour tirer, il ne peut pas tirer
        if (curShotDelay < maxShotDelay)
            return;
        //Si le joueur a attendu le temps nécessaire pour tirer, il peut tirer
        switch (power)
        {
            //cas1: le joueur tire une balle
            case 1: 
            print("case 1");
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
                break;
            //cas 2: le joueur tire 3 balles
            case 2:
            print("case 2");
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
                break;
        }
        curShotDelay = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
{
    //Si le joueur entre en collision avec un objet "Border", tout reste normal 
    if (collision.gameObject.tag == "Border")
    {
        switch (collision.gameObject.name)
        {
            case "Top":
                isTouchTop = true;
                break;
            case "Bottom":
                isTouchBottom = true;
                break;
            case "Right":
                isTouchRight = true;
                break;
            case "Left":
                isTouchLeft = true;
                break;
        }
    }
    //Si le joueur entre en collision avec l'enemy il perd une vie
    else if (collision.gameObject.tag == "Enemy")
    {
        life--;
        manager.UpdateLifeIcon(life);
        //si le nombre de vies est inférieur ou égale à 0 => Game over
        if (life <= 0)
        {
            Destroy(collision.gameObject);
            manager.GameOver();
        }
        //Sinon on respawn le joueur
        else
        {
            manager.RespawnPlayer();
            gameObject.SetActive(false);
        }
        manager.GameOver();
        Destroy(collision.gameObject);
    }
}

void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border"){
            switch (collision.gameObject.name){
                case "Top":
                isTouchTop = false;
                    break;
                case "Bottom":
                isTouchBottom = false;
                    break;
                case "Right":
                isTouchRight = false;
                    break;
                case "Left":
                isTouchLeft = false;
                    break;

            }
        }

    }

//Fin

}


    
