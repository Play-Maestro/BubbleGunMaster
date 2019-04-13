using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWindow : MonoBehaviour
{
    public GameObject wallOfDoom;
    public GameObject player;
    public float speed = 1;
    private float minSpeed = 0.1f;
    private float distance = 0;
    public PauseScreen pause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.getIsPaused())
        {
            //Move faster depending on how far the player is from the wall

            if (player)
            {
                distance = player.transform.position.x - wallOfDoom.transform.position.x;
            }
            minSpeed += (distance / 20000);
            speed = Mathf.Clamp((speed + ((distance - 10) / 100)), minSpeed, minSpeed * 5);

            transform.Translate(Vector3.right * 0.001f * speed);
            wallOfDoom.transform.Translate(Vector3.right * 0.001f * speed);
        }
    }
}
