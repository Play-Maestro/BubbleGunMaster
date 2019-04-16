using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDoom : MonoBehaviour
{
    public SceneController sceneController;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            sceneController.YouLose();
        }
        else if (collision.gameObject.GetComponent<Block>())
        {
            collision.gameObject.GetComponent<Block>().justDelete();
        }
    }

    
}
