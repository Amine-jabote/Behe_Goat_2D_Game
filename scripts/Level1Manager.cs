using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Level1Manager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public Image[] lifeImage;
    public GameObject gameOverSet;
    public GameObject player;
    public Text scoreText;
    
    public static Level1Manager instance;

    public float elapsedTime;
    public Text elapsedTimeText;
    public Text timerText;
    public float maxTime = 160f; // final value of elapsed time

    void Start() {
    elapsedTimeText = GameObject.Find("Chrono").GetComponent<Text>();
}

    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        elapsedTime += Time.deltaTime;
         // Mettre à jour le texte du temps écoulé
        elapsedTimeText.text = string.Format("Time: {0}:{1:00}", (int)elapsedTime / 60, (int)elapsedTime % 60);
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
        // update the timer text
        TimeSpan timeSpan = TimeSpan.FromSeconds(maxTime - elapsedTime);
        string timerString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        timerText.text = timerString;
    }

        if(curSpawnDelay > maxSpawnDelay)
        {
            spawnEnemy();
            maxSpawnDelay = UnityEngine.Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        Ctrl_Player playerLogic = player.GetComponent<Ctrl_Player>();
        scoreText.text = string.Format("{0:n0}");

        if (playerLogic.life == 0)
    {
        GameOver();
    }
    
    }

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

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

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

 public void GameOver()
{
    if (player.GetComponent<Ctrl_Player>().life == 0)
    {
        gameOverSet.SetActive(true);
        player.SetActive(false);
    }
}

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
}
