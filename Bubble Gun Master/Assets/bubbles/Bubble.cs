using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hitSomething)
        {
            return;
        }
        else
        {
            hitSomething = true;
        }
        if (collision.gameObject.tag == "Doom")
        {
            Instantiate(popPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            return;
        }
        Block block = collision.gameObject.GetComponent<Block>();
        Vector3 spawnLoc = Vector3.zero;
        if (block != null && block.myColor != Block.bColor.WALL)
        {
            spawnLoc = block.nearestSpace(transform.position);
            block.Refresh();
        }
        else
        {
            float locX = Mathf.Round(transform.position.x);
            float locY = Mathf.Round(transform.position.y);
            float locZ = Mathf.Round(transform.position.z);
            spawnLoc = new Vector3(locX, locY, locZ);
        }
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
