using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs;


    // Start is called before the first frame update
    void Start()
    {

        for(int x = -4; x < 5; x++)
        {
            for(int y = -4; y < 5; y++)
            {
                Vector3 spawnLocation = transform.position + (Vector3.right * x) + (Vector3.up * y);
                int randomNumber = Random.Range(-4, blockPrefabs.Length);
                if (randomNumber >= 0)
                {
                    Instantiate(blockPrefabs[randomNumber], spawnLocation, Quaternion.identity);
                }
            }
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
