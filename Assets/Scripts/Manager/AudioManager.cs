using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get => _instance;
    }

    private AudioSource _audioSource;
    private bool sound = true;
    public GameObject SoundOnObj;
    public GameObject SoundOffObj;

    void OnEnable()
    {
        EventManager.onLevelFailed += SoundOnOff;
    }

    void OnDisable()
    {
        EventManager.onLevelFailed -= SoundOnOff;
    }
    
    void Awake()
    {
        MakeSingleton();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    void MakeSingleton()
    {
        if(_instance != null) Destroy(gameObject);
        else
        {
            _instance = this;
            //DontDestroyOnLoad(_instance); // instance referanslı objeyi sahne yenilendiğinde yok etme
        }
    }

    public void PlayClipFx(AudioClip audioClip,float volume)
    {
        if(sound) _audioSource.PlayOneShot(audioClip,volume);
    }

    public void SoundOnOff()
    {
        sound = !sound;
        if (sound)
        {
            SoundOnObj.SetActive(true);
            SoundOffObj.SetActive(false);
        }
        else if(!sound) 
        {
            SoundOffObj.SetActive(true);
            SoundOnObj.SetActive(false);
        }
    }
}
