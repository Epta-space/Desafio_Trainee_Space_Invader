using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjetil : MonoBehaviour
{
    public float velocidade;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }


}
