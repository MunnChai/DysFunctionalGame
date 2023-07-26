using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySoundEffect(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip, 0.5f);
    }
}
