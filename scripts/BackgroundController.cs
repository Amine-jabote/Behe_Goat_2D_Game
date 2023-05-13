// Ce script permet de faire défiler le background du jeu  "Behe Goat".
// Auteur: Jabote Mohamed Amine.
// Date: 2023-04-01.
// Dans le cadre de l'UE GL01 à l'UTT.
// Semestre : P23.
// Projet : Projet de jeu vidéo "Behe Goat".
//Langage: C#.


//Appel des bibliothèques
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Début
public class BackgroundController : MonoBehaviour
{
    //Déclaration des variables
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewWidth;

    //Initialisation
    private void Awake()
    {
        viewWidth = Camera.main.orthographicSize * 2;
    }

    //Mise à jour
    void Update()
    {
        //On déplace le sprite
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.left * speed *Time.deltaTime;
        transform.position = curPos  + nextPos;
        
        //Si le sprite est hors de la vue, on le replace à la fin
        if(sprites[endIndex].position.x < viewWidth*(-1))
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.right * viewWidth;

            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = startIndexSave-1 == -1 ? sprites.Length-1 : startIndexSave - 1;
        }
    }
    //Fin
}
