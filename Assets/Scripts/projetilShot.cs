using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetilShot : MonoBehaviour
{
    public GameObject projetilPrefab;
    void Start()
    {
        
    }

    void Update()
    {
         if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(projetilPrefab, transform.position, Quaternion.identity);
        }
    }
}
