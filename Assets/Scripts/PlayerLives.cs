using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLives : MonoBehaviour
{
    public int vidas = 3;
    public Image[] livesUI;
    public GameObject explosionPrefab;
    public int points;
    private PointManager pointManager;
    public TMP_Text ScoreText;
    public GameObject painelGameOver;
    
    void Start()
    {
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    void Update()
    {
        points = GameObject.Find("PointManager").GetComponent<PointManager>().score;
    }

    public void Setup(int score)
    {
        painelGameOver.SetActive(true);
        ScoreText.text = "SCORE: " + score.ToString() + " POINTS";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Instantiate(explosionPrefab,transform.position,Quaternion.identity);
            vidas -= 1;
            for(int i = 0; i < livesUI.Length; i++)
            {
                if(i < vidas)
                {
                    livesUI[i].enabled = true;
                }
                else
                {
                    livesUI[i].enabled = false;
                }
            }
            if(vidas <= 0)
            {
                Destroy(gameObject);
                Setup(points);
            }
        }

        if(collision.gameObject.tag == "projetil Inimigo")
        {
            Destroy(collision.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            vidas -= 1;
            for (int i = 0; i < livesUI.Length; i++)
            {
                if (i < vidas)
                {
                    livesUI[i].enabled = true;
                }
                else
                {
                    livesUI[i].enabled = false;
                }
            }
            if (vidas <= 0)
            {
                Destroy(gameObject);
                Setup(points);
            }
        }
    }



  
}
