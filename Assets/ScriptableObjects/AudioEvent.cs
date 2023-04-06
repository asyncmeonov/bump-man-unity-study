using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{

    public Vector2 volume;
    public Vector2 pitch;

    public bool loop;

    public bool isSpaciallyAware;

    public abstract GameObject Play(AudioSource source);
}
