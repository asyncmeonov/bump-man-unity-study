using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXOrderType
{
    Concecutive,
    Random
}


[CreateAssetMenu(fileName = "sfx_newAudioEvent", menuName = "Audio/New SFX Event")]
public class SFXAudioEvent : AudioEvent
{
    #region config
    public AudioClip[] clips;
    public SFXOrderType playbackType;

    //The index of the clip last played
    private int lastPlayed = 0;
    #endregion

    public override AudioSource Play(AudioSource audioSourceParam = null)
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
            case SFXOrderType.Concecutive:
                source.clip = (lastPlayed == clips.Length - 1) ? clips[0] : clips[lastPlayed + 1];
                break;
            case SFXOrderType.Random:
                source.clip = clips[Random.Range(0, clips.Length)];
                break;

        }
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);

        source.Play();

        //Save what was last played TODO -> monitor if the desctruction of the object after playing keeps this
        lastPlayed = System.Array.IndexOf(clips, source.clip);


#if UNITY_EDITOR
        if (source.gameObject.name != "Audio preview")
        {
            Destroy(source.gameObject, source.clip.length / source.pitch);
        }
#else
                Destroy(source.gameObject, source.clip.length / source.pitch);
#endif

        //return configurations if we want to modify them externally
        return source;
    }
}
