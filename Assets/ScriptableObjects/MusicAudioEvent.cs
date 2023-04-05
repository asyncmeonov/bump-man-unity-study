using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sfx_newMusicEvent", menuName = "Audio/New Music Event")]
public class MusicAudioEvent : AudioEvent
{
    #region config
    public AudioClip clip;
    #endregion

    public override AudioSource Play(AudioSource audioSourceParam = null)
    {
        if(clip == null)
        {
            Debug.Log("Missing sound clips for "+ this);
            return null;
        }

        var source = audioSourceParam;
        if (source== null)
        {
            var _obj = new GameObject("Sound", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        source.clip = clip; //use first audio clip in array always
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);

        source.Play();

        //Destroy after playing
        Destroy(source.gameObject, source.clip.length / source.pitch);

        //return configurations if we want to modify them externally
        return source;
    }
}
