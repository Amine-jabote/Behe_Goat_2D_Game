// Ce script permet de gérer les niveaux du jeu "Behe Goat".
//!!ATTENTION : ***Une petite faute de frappe dans le nom de ce script le nom est : "LevelsManager "***!!
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
using UnityEngine.SceneManagement;
using System;

public class Level1Manager : MonoBehaviour
{
    //Déclaration des variables
    public GameObject[] enemyObjs;//tableau des ennemis
    public Transform[] spawnPoints;//tableau des points de spawn

    public float maxSpawnDelay;//temps maximum entre chaque spawn
    public float curSpawnDelay;//temps actuel entre chaque spawn

    public Image[] lifeImage;//tableau des images de vie
    public GameObject gameOverSet;//objet de fin de jeu
    public GameObject player;//objet du joueur
    public Text scoreText;//texte du score
    
    public static Level1Manager instance;

    public float elapsedTime;//temps écoulé depuis le début du niveau
    public Text elapsedTimeText;//texte du temps écoulé
    public Text timerText;//texte du timer
    public float maxTime = 160f; // final value of elapsed time

    void Start() {
    elapsedTimeText = GameObject.Find("Chrono").GetComponent<Text>();//trouve le texte du chrono
}

    void Awake()
    {
        instance = this;
    }



    //Mis à jour
    void Update()
    {
        curSpawnDelay += Time.deltaTime;//temps entre chaque spawn
        elapsedTime += Time.deltaTime;//temps écoulé depuis le début du niveau
         // Mettre à jour le texte du temps écoulé
        elapsedTimeText.text = string.Format("Time: {0}:{1:00}", (int)elapsedTime / 60, (int)elapsedTime % 60);//affiche le temps écoulé

        //Si le temps écoulé est supérieur au temps maximum et que le joueur n'est pas mort, on passe à la scène finale du niveau
        if (elapsedTime >= maxTime && player.GetComponent<Ctrl_Player>().life > 0 && SceneManager.GetActiveScene().name == "level_1")
        {
        SceneManager.LoadScene("fin_level_1");
        }
        else if (elapsedTime >= maxTime && player.GetComponent<Ctrl_Player>().life > 0 && SceneManager.GetActiveScene().name == "level_2")
        {
        SceneManager.LoadScene("fin_level_2");
        }
        else if (elapsedTime >= maxTime && player.GetComponent<Ctrl_Player>().life > 0 && SceneManager.GetActiveScene().name == "level_3")
        {
        SceneManager.LoadScene("fin_level_3");
        }
        else if (elapsedTime >= maxTime && player.GetComponent<Ctrl_Player>().life > 0 && SceneManager.GetActiveScene().name == "level_4")
        {
        SceneManager.LoadScene("fin_level_4");
        }
        else if (elapsedTime >= maxTime && player.GetComponent<Ctrl_Player>().life > 0 && SceneManager.GetActiveScene().name == "level_5")
        {
        SceneManager.LoadScene("fin_level_5");
        }
        else {
        // Mis à jour du timer text
        TimeSpan timeSpan = TimeSpan.FromSeconds(maxTime - elapsedTime);
        string timerString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        timerText.text = timerString;
        }
        //Si   le temps entre chaque spawn est supérieur au temps maximum entre chaque spawn, on fait spawn un ennemi
        if(curSpawnDelay > maxSpawnDelay)
        {
            spawnEnemy();
            maxSpawnDelay = UnityEngine.Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        // Mettre à jour le texte du score
        Ctrl_Player playerLogic = player.GetComponent<Ctrl_Player>();
        scoreText.text = string.Format("{0:n0}");

        //Si le joueur n'a plus de vie, on lance la fonction GameOver
        if (playerLogic.life == 0)
        {
        GameOver();
        }
    
    }

    //Fonction qui permet de faire spawn un ennemi
   void spawnEnemy()
    {
        int ranEnemy;
    if (SceneManager.GetActiveScene().name == "level_5")
    {
        ranEnemy = UnityEngine.Random.Range(0, 2);
    }
    else if (SceneManager.GetActiveScene().name == "level_3")
    {
        ranEnemy = UnityEngine.Random.Range(0, 4);
    }
    else
    {
        ranEnemy = UnityEngine.Random.Range(0, 8);
    }
    int ranPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
    Debug.Log(ranPoint);
    Instantiate(enemyObjs[ranEnemy], 
        spawnPoints[ranPoint].position ,
        spawnPoints[ranPoint].rotation);
    }

    //Fonction qui permet de mettre à jour les images de vie
   public void UpdateLifeIcon(int life)
    {
        for (int i = 0; i < lifeImage.Length; i++)
        {
            if (i < life)
            {
                lifeImage[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                lifeImage[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    //Fonction qui permet de faire respawn le joueur
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }


    //Fonction qui permet de faire respawn le joueur
    void RespawnPlayerExe()
    {
        if (SceneManager.GetActiveScene().name == "level_4")
    {
        player.transform.position = new Vector3(1.5f, 0.5f, 0f);
    }
    else if (SceneManager.GetActiveScene().name == "level_3")
    {
        player.transform.position = new Vector3(13.96f, -0.47f, 0f);
    }
    else
    {
        player.transform.position = Vector3.left * 3.5f;
    }
    player.SetActive(true);
    }

    //La fonction GameOver permet de gérer la fin de jeu
    public void GameOver()
    {
    if (player.GetComponent<Ctrl_Player>().life == 0)
    {
        gameOverSet.SetActive(true);
        player.SetActive(false);
    }
    }

    //La fonction GameRetry permet de gérer le bouton "Retry" de la fin de jeu
    public void GameRetry()
    {
    Scene currentScene = SceneManager.GetActiveScene();
    if (currentScene.name == "level_4")
    {
        SceneManager.LoadScene("level_4");
    }
    else if (currentScene.name == "level_5")
    {
        SceneManager.LoadScene("level_5");
    }
    else if (currentScene.name == "level_3")
    {
        SceneManager.LoadScene("level_3");
    }
    else
    {
        SceneManager.LoadScene(0);
    }
    }

    //Fin
}
