using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1.0f + Random.Range(-30f, 0.1f);
        audioSource.volume = 0.5f + Random.Range(0.0f, 0.25f);
    }
}
