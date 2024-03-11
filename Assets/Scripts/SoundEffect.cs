using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    //Template given from an In-Class Demo!
    //Properties
    private AudioSource audioSource;
    private bool didPlay;


    //Methods
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (didPlay == false) return;
        if (audioSource.isPlaying == false) Destroy(gameObject);
    }

    public void Init(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    public void Play()
    {
        audioSource.Play();
        didPlay = true;
    }

    public void SetToLoop()
    {
        audioSource.loop = true;
    }
}
