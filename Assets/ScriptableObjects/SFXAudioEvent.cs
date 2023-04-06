using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXPlaybackStrategy
{
    Concecutive,
    Random
}

public enum SFXChoiceStrategy
{
    Alternating,
    Random
}


[CreateAssetMenu(fileName = "sfx_newAudioEvent", menuName = "Audio/New SFX Event")]
public class SFXAudioEvent : AudioEvent
{
    #region config
    public AudioClip[] clips;
    public SFXPlaybackStrategy playbackType;
    public SFXChoiceStrategy pitchAlterationType;

    //The index of the clip last played
    private int _lastPlayed = 0;
    private AudioSource _lastUsedSettings = null;
    #endregion

    public override GameObject Play(AudioSource audioSourceParam = null)
    {
        if (clips.Length == 0)
        {
            Debug.Log("Missing sound clips for " + name);
            return null;
        }

        var source = audioSourceParam;
        if (source == null)
        {
            var _obj = new GameObject("SFX Sound Source", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        switch (playbackType)
        {
            case SFXPlaybackStrategy.Concecutive:
                source.clip = (_lastPlayed == clips.Length - 1) ? clips[0] : clips[_lastPlayed + 1];
                break;
            case SFXPlaybackStrategy.Random:
                source.clip = clips[Random.Range(0, clips.Length)];
                break;

        }

        switch (pitchAlterationType)
        {
            case SFXChoiceStrategy.Random:
                source.pitch = Random.Range(pitch.x, pitch.y);
                break;
            case SFXChoiceStrategy.Alternating:
                bool isHeads = Random.Range(1, 100) > 50;
                float stepCoef = (isHeads) ? 0.02f : -0.02f;
                if (_lastUsedSettings != null)
                {
                    //TODO this does not work when looping, which is really when you want it to work
                    source.pitch = Mathf.Clamp(_lastUsedSettings.pitch + stepCoef, pitch.x, pitch.y);
                }
                else
                {
                    source.pitch = Random.Range(pitch.x, pitch.y);
                }
                break;
        }
        source.volume = Random.Range(volume.x, volume.y);

        source.loop = loop;

        if (isSpaciallyAware)
        {
            source.rolloffMode = AudioRolloffMode.Linear;
            source.spatialBlend = 1;
            source.minDistance = 2;
            source.maxDistance = 5;
        }
        else
        {
            source.spatialBlend = 0;
        } 
        source.Play();

        _lastPlayed = System.Array.IndexOf(clips, source.clip);
        _lastUsedSettings = source;


#if UNITY_EDITOR
        if (source.gameObject.name != "Audio preview" && !loop)
        {
            Destroy(source.gameObject, source.clip.length / source.pitch);
        }
#else
        
            Destroy(source.gameObject, source.clip.length / source.pitch);
       
                
#endif

        //return game object with all configs if we want to modify them externally
        return source.gameObject;
    }
}
