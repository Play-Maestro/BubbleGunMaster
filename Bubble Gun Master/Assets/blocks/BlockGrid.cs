using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    Dictionary<string, Block> grid = new Dictionary<string, Block>();
    

    public string makeKey(Vector3 pos)
    {
        int posX = (int)Mathf.Round(pos.x);
        int posY = (int)Mathf.Round(pos.y);
        return posX.ToString() + "x" + posY.ToString();
    }

    private void Start()
    {
        //StartCoroutine(cleanGrid());
    }

    // Add a block to the grid
    public void addToGrid(Vector3 pos, Block block)
    {
        string key = makeKey(pos);
        try
        {
            grid.Add(key, block);
        } catch
        {
            grid[key] = block;
        }
    }
    public void addToGrid(float x, float y, Block block)
    {
        string key = x.ToString() + "x" + y.ToString();
        try
        {
            grid.Add(key, block);
        }
        catch
        {
            grid[key] = block;
        }
    }
    public void addToGrid(string key, Block block)
    {
        try
        {
            grid.Add(key, block);
        }
        catch
        {
            grid[key] = block;
        }
    }

    public void createBlock(GameObject block, Vector3 pos)
    {
        if (!exists(pos))
        {
            GameObject newBlock = Instantiate(block, pos, Quaternion.identity);
            addToGrid(pos, newBlock.GetComponent<Block>());
            Destroy(this.gameObject);
        }
    }

    // Remove a block from the grid
    public void removeFromGrid(Vector3 pos)
    {
        string key = makeKey(pos);
        grid.Remove(key);
    }
    public void removeFromGrid(float x, float y)
    {
        string key = x.ToString() + "x" + y.ToString();
        grid.Remove(key);
    }
    public void removeFromGrid(string key)
    {
        grid.Remove(key);
    }

    public void PopBlock(Vector3 pos)
    {

    }

    // Find out if there is a block there
    public bool exists(Vector3 pos)
    {
        string key = makeKey(pos);
        return grid.ContainsKey(key);
    }
    public bool exists(float x, float y)
    {
        string key = x.ToString() + "x" + y.ToString();
        return grid.ContainsKey(key);
    }
    public bool exists(string key)
    {
        return grid.ContainsKey(key);
    }

    // Get the block in the location
    public Block GetBlock(Vector3 pos)
    {
        string key = makeKey(pos);
        Block block = grid[key];
        return block;
    }
    public Block GetBlock(float x, float y)
    {
        string key = x.ToString() + "x" + y.ToString();
        Block block = grid[key];
        return block;
    }
    public Block GetBlock(string key)
    {
        Block block = grid[key];
        return block;
    }

    public Block[] adjacentBlocks (Vector3 pos)
    {
        string key = makeKey(pos);
        int posX = (int)pos.x;
        int posY = (int)pos.y;
        string[] adjacentKeys = new string[] {
            posX.ToString() + "x" + (posY + 1).ToString(),
            (posX + 1).ToString() + "x" + posY.ToString(),
            posX.ToString() + "x" + (posY - 1).ToString(),
            (posX - 1).ToString() + "x" + posY.ToString() };
        Block[] blocks = new Block[4];
        for (int i = 0; i < blocks.Length; i++)
        {
            if(exists(adjacentKeys[i]))
                blocks[i] = GetBlock(adjacentKeys[i]);
        }
        return blocks;
    }

    public bool IsBlockRooted(Vector3 pos)
    {
        string key = makeKey(pos);
        int posX = (int)pos.x;
        int posY = (int)pos.y;
        List<string> checkedList = new List<string>();
        checkedList.Add(key);
        return IsRooted(key, posX, posY, checkedList);
    }

    private bool IsRooted(string key, int posX, int posY, List<string> checkedList)
    {
        bool rooted = false;
        string[] adjacentKeys = new string[] {
            posX.ToString() + "x" + (posY + 1).ToString(),
            (posX + 1).ToString() + "x" + posY.ToString(),
            posX.ToString() + "x" + (posY - 1).ToString(),
            (posX - 1).ToString() + "x" + posY.ToString() };

        for (int i = 0; i < adjacentKeys.Length; i++)
        {
            if(checkedList.Contains(adjacentKeys[i]))
            {
                continue;
            }
            else if (grid.ContainsKey(adjacentKeys[i]))
            {
                checkedList.Add(adjacentKeys[i]);
                if (grid[adjacentKeys[i]].myColor == Block.bColor.WALL)
                {
                    return true;
                }
                int newX = 0, newY = 0;
                switch(i)
                {
                    case 0:
                        newX = posX;
                        newY = posY + 1;
                        break;
                    case 1:
                        newX = posX + 1;
                        newY = posY;
                        break;
                    case 2:
                        newX = posX;
                        newY = posY - 1;
                        break;
                    case 3:
                        newX = posX - 1;
                        newY = posY;
                        break;
                }
                rooted = IsRooted(adjacentKeys[i], newX, newY, checkedList);
                if (rooted == true)
                    return true;
                
            }
        }
        return rooted;
    }

    private IEnumerator cleanGrid()
    {
        while (true)
        {
            foreach (string key in grid.Keys)
            {
                if (grid[key] == null)
                {
                    grid.Remove(key);
                }
            }
            foreach (string key in grid.Keys)
            {
                print("Key " + key);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
