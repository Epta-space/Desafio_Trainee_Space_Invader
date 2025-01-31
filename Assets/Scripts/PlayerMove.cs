using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float velocidade = 5;
    public float input;

    void Start()
    {
        
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right *input* velocidade * Time.deltaTime);
    }
}
