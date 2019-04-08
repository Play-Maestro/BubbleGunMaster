using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinEffect;
    private float life = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life > 0)
        {
            life -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(coinEffect, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Player>().AddCoin();
            Destroy(this.gameObject);
        }
    }
}
