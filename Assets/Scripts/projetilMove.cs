using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetilMove : MonoBehaviour
{
    public float velocidade;
    public GameObject explosionPrefab;
    private PointManager pointManager;
    void Start()
    {
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            pointManager.UpdateScore(50);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
