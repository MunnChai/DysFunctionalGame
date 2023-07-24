using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip dash, hit, death;

    public void DashSound() {
        audioSource.PlayOneShot(dash, 0.5f);
    }

    public void HitSound() {
        audioSource.PlayOneShot(hit, 1f);
    }

    public void DeathSound() {
        audioSource.PlayOneShot(death, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
