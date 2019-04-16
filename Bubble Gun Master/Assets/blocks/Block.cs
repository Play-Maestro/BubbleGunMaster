using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Block.cs
 * Nathan Jensen
 * 
 * Provides the basic attributes and functions for
 * a block object. The blocks include the 
 * multicolored breakable block and also
 * the blocks that make up the walls.
 */

public class Block : MonoBehaviour
{
    public enum bColor {RED, ORANGE, YELLOW, GREEN, BLUE, PURPLE, WALL};
    public bColor myColor;
    public GameObject bubble;
    public GameObject pop;
    Vector3[] adjacentSpaces = new Vector3[4];
    Block[] adjacentBlocks = new Block[4];
    int sameColorCount = 0;
    BlockGrid grid;
    string myId;

    public GameObject coin;

    
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<BlockGrid>();
        myId = grid.makeKey(transform.position);
        
        // Find out if there is already a block in that space.
        if (grid.exists(myId))
        {
            Destroy(this.gameObject);
            return;
        }

        //Put the block into the grid
        grid.addToGrid(myId, this);

        //The next part does not need to be done if the block is part of the walls
        if (myColor != bColor.WALL)
        {
            adjacentSpaces[0] = transform.position + Vector3.up;
            adjacentSpaces[1] = transform.position + Vector3.right;
            adjacentSpaces[2] = transform.position - Vector3.up;
            adjacentSpaces[3] = transform.position - Vector3.right;

            UpdateBlock();
            foreach (Block block in adjacentBlocks)
            {
                if (block != null)
                    block.UpdateBlock();
            }
            if (!grid.IsBlockRooted(transform.position))
            {
                List<string> bList = new List<string>();
                BackToBubble(bList);
            }
        }
    }

    // This is called when a block is hit. It is similar to
    // what is done in the Start fuction.
    public void Refresh()
    {
        myId = grid.makeKey(transform.position);

        grid.addToGrid(myId, this);
        if (myColor != bColor.WALL)
        {
            adjacentSpaces[0] = transform.position + Vector3.up;
            adjacentSpaces[1] = transform.position + Vector3.right;
            adjacentSpaces[2] = transform.position - Vector3.up;
            adjacentSpaces[3] = transform.position - Vector3.right;

            UpdateBlock();
            foreach (Block block in adjacentBlocks)
            {
                if (block != null)
                    block.UpdateBlock();
            }
            if (!grid.IsBlockRooted(transform.position))
            {
                List<string> bList = new List<string>();
                BackToBubble(bList);
            }
        }
    }

    // This finds out how many blocks adjacent to this one are the same color
    public void UpdateBlock()
    {
        sameColorCount = 0;
        for (int i = 0; i < adjacentSpaces.Length; i ++)
        {
            if(grid.exists(adjacentSpaces[i]))
            {
                adjacentBlocks[i] = grid.GetBlock(adjacentSpaces[i]);
                if (myColor == adjacentBlocks[i].myColor)
                {
                    sameColorCount++;
                }
            }
        }
        Invoke("TryPop", 0.3f);
    }

    // This is used when a bubble hits this block.
    // It will return an adjacent space closest to 
    // the bubble.
    public Vector3 nearestSpace(Vector3 bubPos)
    {
        int nearestNumber = 0;
        float nearestDistance = 100.0f;
        for(int i = 0; i < adjacentSpaces.Length; i++)
        {
            float myDist = Vector3.Distance(adjacentSpaces[i], bubPos);
            if (myDist < nearestDistance)
            {
                nearestDistance = myDist;
                nearestNumber = i;
            }
        }

        return adjacentSpaces[nearestNumber];
    }

    // Little function to test if the block has enough 
    // Adjacent colors to pop.
    private void TryPop() {
        if (sameColorCount >= 2)
        {
            List<string> popList = new List<string>();
            popList.Add(myId);
            Pop(popList);
        }
    }

    // Will cause adjacent blocks of the same color to pop as well as this block
    public void Pop(List<string> popList)
    {      
        foreach (Block block in grid.adjacentBlocks(transform.position))
        {
            if (block != null)
            {
                // Will call this function on other adjacent blocks of the same color
                if (block.myColor == myColor &&
                popList.Contains(block.myId) == false)
                {
                    popList.Add(block.myId);
                    block.Pop(popList);
                }
            }
        }
        grid.removeFromGrid(myId);
        // Update the other blocks next to this one
        foreach (Block block in grid.adjacentBlocks(transform.position))
        {
            if (block != null && block.myColor != myColor &&
                block.myColor != bColor.WALL)
            {
                block.Invoke("TestForConnect", 0.2f);               
            }
        }
        //Pop after a short delay
        Invoke("DoThePop", 0.1f);
    }

    public void TestForConnect()
    {
        // use function from the grid to see if this block has a connection to the walls
        if (!grid.IsBlockRooted(transform.position))
        {
            List<string> bList = new List<string>();
            bList.Add(myId);
            BackToBubble(bList);
        }
    }

    // Actually pop the block. Small chance to spawn a coin
    private void DoThePop()
    {
        Instantiate(pop, transform.position, Quaternion.identity);
        if(Random.Range(0,10) <= 3)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    // Similar to popping, but can be used for walls, 
    // Does not spawn coins.
    public void justDelete()
    {
        grid.removeFromGrid(myId);
        if (pop)
        {
            Instantiate(pop, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    //Makes the block go back to a bubble.
    public void BackToBubble(List<string> bList)
    {
        foreach (Block block in grid.adjacentBlocks(transform.position))
        {
            // Make all other connected blocks that are not walls also go to bubble.
            if (block != null && block.myColor != bColor.WALL &&
                bList.Contains(block.myId) == false)
            {
                bList.Add(block.myId);
                block.BackToBubble(bList);
            }
        }
        grid.removeFromGrid(myId);
        Instantiate(bubble, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
