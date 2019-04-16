using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BlockSpawner.cs
 * Nathan Jensen
 * 
 * This is attached to an object. When that object is created
 * this will create a grid of randomly placed blocks
 * 
 * TODO - Maybe make a way to set the difficulty? Number of air spaces?
 * 
 */

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        // grid area of 9x9
        for(int x = -4; x < 5; x++)
        {
            for(int y = -4; y < 5; y++)
            {
                Vector3 spawnLocation = transform.position + (Vector3.right * x) + (Vector3.up * y);
                // when set to -4, there is a chance to spawn blank spaces
                int randomNumber = Random.Range(-4, blockPrefabs.Length);
                if (randomNumber >= 0)
                {
                    Instantiate(blockPrefabs[randomNumber], spawnLocation, Quaternion.identity);
                }
            }
        }
        Destroy(this.gameObject);
        //Don't need this after it is done spawning blocks
    }
}
