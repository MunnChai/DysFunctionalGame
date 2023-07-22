using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mouseClick, buttonPress;

    public void PlayButtonPress() {
        audioSource.clip = buttonPress;
        audioSource.Play();
    }
}
