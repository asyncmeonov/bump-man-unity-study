using UnityEngine;
using UnityEngine.Audio;

public abstract class AudioEvent : ScriptableObject
{

    public Vector2 volume;
    public Vector2 pitch;

    public bool loop;

    public bool isSpaciallyAware;

    public AudioMixerGroup mixGroup;

    public abstract GameObject Play(AudioSource source);
}
