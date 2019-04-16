using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Block.cs
 * Nathan Jensen
 * 
 * Provides the basic attributes and functions for
 * a bubble object. 
 * 
 */

public class Bubble : MonoBehaviour
{
    public GameObject blockPrefab;
    private BlockGrid grid;
    public GameObject popPrefab;
    private bool hitSomething = false;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<BlockGrid>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hitSomething) //This means that the bubble has already hit something. Prevents bug where two blocks spawn.
        {
            return;
        }
        else
        {
            hitSomething = true;
        }

        //The spike wall will cause the bubble to pop without spawning a block
        if (collision.gameObject.tag == "Doom")
        {
            Instantiate(popPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            return;
        }

        // If it hit a block, update that block and spawn a block in an adjacent location
        Block block = collision.gameObject.GetComponent<Block>();
        Vector3 spawnLoc = Vector3.zero;
        if (block != null && block.myColor != Block.bColor.WALL)
        {
            spawnLoc = block.nearestSpace(transform.position);
            block.Refresh();
        }
        // Otherwise, just spawn a block in this general location
        else
        {
            float locX = Mathf.Round(transform.position.x);
            float locY = Mathf.Round(transform.position.y);
            float locZ = Mathf.Round(transform.position.z);
            spawnLoc = new Vector3(locX, locY, locZ);
        }
        // Don't spawn if there is already a block there
        if (!grid.exists(spawnLoc))
        {
            Instantiate(blockPrefab, spawnLoc, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(popPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
