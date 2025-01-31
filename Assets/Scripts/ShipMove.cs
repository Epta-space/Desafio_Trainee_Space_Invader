using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float velocidade;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            velocidade *= -1;
        }
    }
}
