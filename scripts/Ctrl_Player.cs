using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Player : MonoBehaviour
{
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


    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchTop && h == 1) || (isTouchBottom && h == -1)){
            h = 0;
        }
        
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchRight && v == -1) || (isTouchLeft && v == 1)){
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

       
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Fire();
    }

    curShotDelay += Time.deltaTime;

        
    }

    void Fire()
    {
        print("Fire");
       /* if (!Input.GetButton(KeyCode.Space.ToString()))
            return;*/
        if (curShotDelay < maxShotDelay)
            return;
        switch (power)
        {
            case 1: 
            print("case 1");
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
                break;
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
    else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
    {
        life--;
        manager.UpdateLifeIcon(life);
        if (life <= 0)
        {
            Destroy(collision.gameObject);
            manager.GameOver();
        }
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



}


    