using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{

    public Vector2 volume;
    public Vector2 pitch;

    public abstract AudioSource Play(AudioSource source);
}
