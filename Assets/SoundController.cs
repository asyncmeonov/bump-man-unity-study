using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Turns out this is not how you use AudioSources. They are meant only for one sound clip only!
//Brutally hacking this together before I figure out how to use scriptable objects to store the data
//for sound files and create a proper sound manager class that can create sources on demand in a cleaner fashion

//Bumped in priority if we want filters. Filters are applied to a game object, the sound controller should be a factory for instances
//of sound sources and applying filters and modifs ontop
public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    private List<AudioSource> loadedSources;


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
        //_audioSource = GetComponent<AudioSource>();
        loadedSources = new List<AudioSource>();
    }

    public void PlaySound(AudioClip clip, bool repeat = false, float pitch = 1f)
    {
        AudioSource source;
        #nullable enable
        AudioSource? existingSource = loadedSources.Find(sor => sor.clip.name == clip.name);
        if(existingSource == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.pitch = pitch;
            source.loop = repeat;
            loadedSources.Add(source);
        }
        else
        {
            source = existingSource;
        }

        source.Play();


    }
    public void PlayRandomSound(AudioClip[] clips, bool repeat = false, float pitch = 1f) => PlaySound(clips[Random.Range(0, clips.Length)], repeat, pitch);


}
