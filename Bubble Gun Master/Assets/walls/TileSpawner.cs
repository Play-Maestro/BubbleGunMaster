using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject wallOfDoom;
    public GameObject wallTile;
    public GameObject blockSpawner;

    public List<GameObject> wallTileList = new List<GameObject>();

    void Update()
    {
        if (NeedToSpawn())
        {
            Vector3 spawnLocation = wallTileList[(wallTileList.Count - 1)].transform.position + (Vector3.right * 9);
            GameObject newTile = Instantiate(wallTile, spawnLocation, Quaternion.identity);
            Instantiate(blockSpawner, spawnLocation, Quaternion.identity);
            wallTileList.Add(newTile);
            if(wallTileList.Count >= 5)
            {
                GameObject wallToDestroy = wallTileList[0];
                wallTileList.RemoveAt(0);
                Destroy(wallToDestroy);
            }
        }
    }

    bool NeedToSpawn()
    {
        return (wallTileList[(wallTileList.Count - 2)].transform.position.x < mainCamera.transform.position.x);
    }

    
}

