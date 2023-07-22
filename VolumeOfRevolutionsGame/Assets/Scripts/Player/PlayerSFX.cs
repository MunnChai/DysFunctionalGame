using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip dash;

    public void DashSound() {
        audioSource.clip = dash;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
