using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    //Sound Library
    [SerializeField] private AudioClip[] pickupSounds;


    public static SoundController Instance {get; private set;}

    private AudioSource _audioSource;
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void PlayPickupSound()
    {
        _audioSource.PlayOneShot(pickupSounds[Random.Range(0, pickupSounds.Length)]);
    }


}
