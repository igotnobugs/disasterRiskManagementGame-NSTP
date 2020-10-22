using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundScipt : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip hoverSound;

    public float volumeClip = 1.0f;

    void Start() {
        audioSource = GetComponent<AudioSource>();     
    }

    public void ClickSound() {
        audioSource.clip = clickSound;
        audioSource.volume = volumeClip;
        audioSource.Play();
    }

    public void HoverSound() {
        audioSource.clip = hoverSound;
        audioSource.volume = volumeClip;
        audioSource.Play();
    }
}
