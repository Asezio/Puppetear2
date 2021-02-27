﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public List<AudioClip> sounds;
    public static SoundManager instance;

    private ObjectPool soundPool;
    private readonly Dictionary<string, AudioClip> nameToSound = new Dictionary<string, AudioClip>();

    public BackgroundMusic bgMusic;

    ///<summary>
    ///Unity's Awake method.
    ///</summary>
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        soundPool = GetComponent<ObjectPool>();
    }

    ///<summary>
    ///Unity's Start method.
    ///</summary>
    private void Start()
    {
        foreach (var sound in sounds)
        {
            nameToSound.Add(sound.name, sound);
        }
        bgMusic = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>();
    }

    ///<summary>
    ///Adds the specified list of sounds to the system.
    ///</summary>
    ///<param name="soundsToAdd">The sounds to add to the system.</param>
    public void AddSounds(List<AudioClip> soundsToAdd)
    {
        foreach (var sound in soundsToAdd)
        {
            nameToSound.Add(sound.name, sound);
        }
    }

    ///<summary>
    ///Removes the specified list of sounds from the system.
    ///</summary>
    ///<param name="soundsToAdd">The sounds to add to the system.</param>
    public void RemoveSounds(List<AudioClip> soundsToAdd)
    {
        foreach (var sound in soundsToAdd)
        {
            nameToSound.Remove(sound.name);
        }
    }

    ///<summary>
    ///Plays the specified audio clip.
    ///</summary>
    ///<param name="clip">The audio clip to play.</param>
    ///<param name="loop">True if the clip should be looped; false otherwise.</param>
    
    public void PlaySound(AudioClip clip, bool loop = false)
    {
        
        if (clip != null)
        {Debug.LogError(1);
            soundPool.GetObject().GetComponent<SoundFX>().Play(clip, loop);
        }
    }

    ///<summary>
    ///Plays the sound with the specefied name.
    ///</summary>
    ///<param name="soundName">The name of the sound to play</param>
    ///<param name="loop">True if the clip should be looped; false otherwise.</param>
    public void PlaySound(string soundName, bool loop = false)
    {
        var clip = nameToSound[soundName];
        if (clip != null)
        {
            PlaySound(clip, loop);
        }
    }

    ///<summary>
    ///Plays the sound with the specified name.
    ///</summary>
    ///<param name="soundName">The name of the sound to play</param>
    ///<param name="time">the time to play clip for before invoking disable</param>
    ///<param name="loop">True if the clip should be looped; false otherwise.</param>
    public void PlayLoopedSound(string soundName, bool loop = false, float time = 0f)
    {
        var clip = nameToSound[soundName];
        if (clip != null)
        {
            if (time <= 0f)
            {
                time = clip.length;
            }
            soundPool.GetObject().GetComponent<SoundFX>().Play(clip, loop, time);
        }
    }

    ///<summary>
    ///Stops a currently playing audio clip in the attached audioSource component to this object/script.
    ///</summary>
    public void StopCurrentClip()
    {
        soundPool.GetComponent<SoundFX>().StopCurrentClip();
    }

    ///<summary>
    ///Set the sound as enabled/disabled.
    ///</summary>
    ///<param name="soundEnable">True if the sound should be enabled; false otherwise.</param>
    public void SetSoundEnabled(bool soundEnabled)
    {
        PlayerPrefs.SetInt("sound_enable", soundEnabled ? 1 : 0);
    }

    ///<summary>
    ///Sets the music as enabled/disabled.
    ///</summary>
    ///<param name="musicEnable">True if the music should be enabled; false otherwise.</param>
    public void SetMusicEnabled(bool musicEnabled)
    {
        PlayerPrefs.SetInt("music_enabled", musicEnabled ? 1 : 0);
        bgMusic.GetComponent<AudioSource>().mute = !musicEnabled;
    }

    ///<summary>
    ///Toggles the sound.
    ///</summary>
    public void ToggleSound()
    {
        var sound = PlayerPrefs.GetInt("sound_enabled");
        PlayerPrefs.SetInt("sound_enabled", 1 - sound);
    }

    ///<summary>
    ///Toggles the music.
    ///</summary>
    public void ToggleMusic()
    {
        var music = PlayerPrefs.GetInt("music_enabled");
        PlayerPrefs.SetInt("sound_enabled", 1 - music);
        bgMusic.GetComponent<AudioSource>().mute = (1 - music) == 0;
    }
}
