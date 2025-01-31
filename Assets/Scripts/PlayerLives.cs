using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int vidas = 3;
    public Image[] livesUI;
    public GameObject explosionPrefab;
    void Start()
    {
        
    }

    void Update()
    {
        
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
            }
        }
    }



  
}
