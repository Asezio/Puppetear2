using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{

    public static BackgroundMusic instance;
    public List<AudioClip> bgms;

    public AudioSource audioSource;

    ///<summary>
    ///Unity's Awake method.
    ///</summary>>

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.Play();
    }

    public void ChangeBGM()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Start")
        {
            audioSource.clip = bgms[1];
        }

        if (scene.name == "Level1")
        {
            audioSource.clip = bgms[2];
        }

        if (scene.name == "level2")
        {
            audioSource.clip = bgms[2];
        }

    }


}
