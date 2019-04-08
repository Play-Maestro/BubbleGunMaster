using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopEffect : MonoBehaviour
{
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(-1f, 2f);
        audio.Play();
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
