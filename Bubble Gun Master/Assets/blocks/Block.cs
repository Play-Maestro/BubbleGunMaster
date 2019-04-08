using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        if (grid.exists(myId))
        {
            Destroy(this.gameObject);
        }
        grid.addToGrid(myId, this);
        if (myColor != bColor.WALL)
        {
            adjacentSpaces[0] = transform.position + Vector3.up;
            adjacentSpaces[1] = transform.position + Vector3.right;
            adjacentSpaces[2] = transform.position - Vector3.up;
            adjacentSpaces[3] = transform.position - Vector3.right;

            updateBlock();
            foreach (Block block in adjacentBlocks)
            {
                if (block != null)
                    block.updateBlock();
            }
            if (!grid.IsBlockRooted(transform.position))
            {
                List<string> bList = new List<string>();
                BackToBubble(bList);
            }
        }
    }

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

            updateBlock();
            foreach (Block block in adjacentBlocks)
            {
                if (block != null)
                    block.updateBlock();
            }
            if (!grid.IsBlockRooted(transform.position))
            {
                List<string> bList = new List<string>();
                BackToBubble(bList);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateBlock()
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

    private void TryPop() {
        if (sameColorCount >= 2)
        {
            Pop(new List<string>());
        }
    }

    public void Pop(List<string> popList)
    {
        popList.Add(myId);
        foreach (Block block in grid.adjacentBlocks(transform.position))
        {
            //print(myId + " Block");
            if (block != null)
            {
                //print(myId + " Good " + block.myId);
                if (block.myColor == myColor &&
                popList.Contains(block.myId) == false)
                {
                    //print(myId + " block " + block.myId + " matches my color.");
                    block.Pop(popList);
                }
            }
        }
        grid.removeFromGrid(myId);
        foreach (Block block in grid.adjacentBlocks(transform.position))
        {
            if (block != null && block.myColor != myColor &&
                block.myColor != bColor.WALL)
            {
                block.Invoke("TestForConnect", 0.2f);               
            }
        }
        //print(myId + " POP");
        Invoke("DoThePop", 0.1f);
    }

    public void TestForConnect()
    {

        if (!grid.IsBlockRooted(transform.position))
        {
            List<string> bList = new List<string>();
            BackToBubble(bList);
        }
    }

    private void DoThePop()
    {
        Instantiate(pop, transform.position, Quaternion.identity);
        if(Random.Range(0,10) <= 3)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    public void justDelete()
    {
        grid.removeFromGrid(myId);
        if (pop)
        {
            Instantiate(pop, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    public void BackToBubble(List<string> bList)
    {
        bList.Add(myId);
        
        foreach (Block block in grid.adjacentBlocks(transform.position))
        {
            if (block != null && block.myColor != bColor.WALL &&
                bList.Contains(block.myId) == false)
            {
                block.BackToBubble(bList);
            }
        }
        grid.removeFromGrid(myId);
        Instantiate(bubble, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
