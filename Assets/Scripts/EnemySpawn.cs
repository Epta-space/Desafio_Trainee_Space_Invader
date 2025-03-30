using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemys;
    float spawnTimer = 10;
    void Start()
    {
        
    }

    
    void Update()
    {
        
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            Instantiate(enemys, new Vector3(-6,1,0), Quaternion.identity);
            spawnTimer = 10;
        }

    }
}
