using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSounds : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public AudioClip collisionClip;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = collisionClip;
    }

    void OnCollisionEnter(Collision collision) {
        float audioLevel;
        if (collision.relativeVelocity.magnitude > 0.5f) {
            audioLevel = collision.relativeVelocity.magnitude / 10.0f;
            audioSource.volume = audioLevel;
            audioSource.Play();
        }
    }
}
