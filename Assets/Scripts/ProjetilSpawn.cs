using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilSpawn : MonoBehaviour
{
    public GameObject projetilInimigo;
    public float spawnTimer;
    public float spawnMax = 10;
    public float spawnMin = 5;
    void Start()
    {
        spawnTimer = Random.Range(spawnMin, spawnMax);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            Instantiate(projetilInimigo, transform.position, Quaternion.identity);
            spawnTimer = Random.Range(spawnMin,spawnMax);
        }

    }
}
