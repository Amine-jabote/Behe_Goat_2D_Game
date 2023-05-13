// Ce script permet de gérer les bullets lancé par le joueur dans les niveaux du jeu "Behe Goat".
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

//Début
public class Bullet : MonoBehaviour
{
    //Déclaration des variables
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Si l'objet "Bullet" entre en collision avec un objet "Enemy" alors on détruit l'objet "Bullet" et on inflige des dégats à l'objet "Enemy"
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
    //Fin
}
