﻿using UnityEngine;
using UnityEngine.Assertions;


public class SoundFX : MonoBehaviour {
    private AudioSource audioSource;

    ///<summary>
    ///Unity's Awake method.
    ///</summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsTrue(audioSource != null);
    }

    ///<summary>
    ///Plays the specified audio clip.
    ///</summary>
    ///<param name="clip">The audio clip to play.</param>
    ///<param name="loop">True if the clip should be looped; false otherwise.</param>
    public void Play(AudioClip clip, bool loop = false)
    {
        if (clip == null)
        {
           return;
        }
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.spatialBlend = 0;

        audioSource.Play();
        if (!loop)
        {
            Invoke("DisableSoundFx", clip.length + 0.1f);
        }
    }

    ///<summary>
    ///Plays the specified audio clip for Time(n).
    ///</summary>
    ///<param name="clip">The audio clip to play.</param>
    ///<param name="time">The time to play clip for before invoking disable.</param>
    ///<param name="loop">True if the clip should be looped; false otherwise.</param>
    
    public void Play(AudioClip clip, bool loop = false,  float time = 0f)
    {
        if (clip == null)
        {          
            return;
        }
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.spatialBlend = 0;

        audioSource.Play();
        if (!loop)
        {
            Invoke("DisableSoundFx", clip.length + 0.1f);
        }
        else
        {
            Invoke("DisableSoundFx", time);
        }

    }

    public void Play3D(AudioClip clip, bool loop = false, float time = 0f)
    {
        if (clip == null)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = 3;

        audioSource.Play();
        if (!loop)
        {
            Invoke("DisableSoundFx", clip.length + 0.1f);
        }
        else
        {
            Invoke("DisableSoundFx", time);
        }

    }


    ///<summary>
    ///Stops a currently playing audio clip in the attached audioSource component to this object/script.
    ///</summary>
    public void StopCurrentClip()
    {
        audioSource.loop = false;
        audioSource.Stop();
        Invoke("DisableSoundFx", audioSource.clip.length + 0.1f);
    }

    /// <summary>
    /// Returns the sound effect to the sound effects pool.
    /// </summary>
    private void DisableSoundFx()
    {
        GetComponent<PooledObject>().pool.ReturnObject(gameObject);
    }
}
