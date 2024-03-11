using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Template given from a Class-Demo!
    //Properties
    private static SoundManager instance;
    private AudioSource audioSource;

    [Header("Prefabs")]
    [SerializeField] private GameObject soundEffectPrefab;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip chingClip;
    [SerializeField] private AudioClip crowdClip;
    [SerializeField] private AudioClip noDealClip;


    //Enums
    public enum SoundType
    {
        CHING = 0,
        CROWDCHEER = 1,
        NODEAL = 2,
    }

    //Methods
    void Awake()
    {
        //Getting the components and instance
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { Debug.LogError("The audio source component is not attached to this object!"); }
        DontDestroyOnLoad(this);
        instance = this;
    }

    public static void PlaySound(SoundType s)
    {
        instance.PrivatePlaySound(s);
    }

    private void PrivatePlaySound(SoundType s)
    {
        AudioClip clip = null;

        //Matching our SoundType enum to our clips
        switch (s)
        {
            case SoundType.CHING:
                clip = chingClip;
                break;
            case SoundType.CROWDCHEER:
                clip = crowdClip;
                break;
            case SoundType.NODEAL:
                clip = noDealClip;
                break;
        }

        //Making a new sound effect
        GameObject soundEffectObject = Instantiate(soundEffectPrefab);
        SoundEffect soundEffect = soundEffectObject.GetComponent<SoundEffect>();

        //Looping our clip if it's the crowd one (Happens at end of game)
        if (clip == crowdClip)
        {
            soundEffect.SetToLoop();
        }

        soundEffect.Init(clip);
        soundEffect.Play();
    }
}
