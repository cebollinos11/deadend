using UnityEngine;
using System.Collections;
using System.Collections.Generic;





public class AudioManager : Singleton<AudioManager>
{
    AudioSource mainAudioSource;

    public AudioClip mainSong;
    public AudioClip swing;
    public AudioClip playerGetHit;
    public AudioClip hammerDown;
    public AudioClip coin;

    public static void PlayClip(AudioClip aClip)
    {
        Instance.mainAudioSource.PlayOneShot(aClip);

    }

    public static void PlayBgSong(AudioClip bg)
    {

        //Instance.mainAudioSource.PlayOneShot(Instance.backgroundSongs[i]);
        Instance.mainAudioSource.clip = bg;
        Instance.mainAudioSource.loop = true;
        Instance.mainAudioSource.Play();

    }





    // Use this for initialization
    void Start()
    {
        mainAudioSource = Instance.gameObject.AddComponent<AudioSource>();

        GameObject go = GameObject.Find("yoloaudio");

        if (go != null && go != gameObject)
            Destroy(gameObject);
        else
        {
            PlayBgSong(mainSong);
        }

        gameObject.name = "yoloaudio";
        DontDestroyOnLoad(this.gameObject);
       

    }

   




}