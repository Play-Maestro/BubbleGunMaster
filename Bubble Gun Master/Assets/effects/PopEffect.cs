using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopEffect : MonoBehaviour
{
    private AudioSource popAudio;
    // Start is called before the first frame update
    void Start()
    {
        popAudio = GetComponent<AudioSource>();
        popAudio.pitch = Random.Range(-0.7f, 1.3f);
        popAudio.Play();
        Destroy(this.gameObject, 1f);
    }
}
