using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MyAudio
{
    public string id;
    public AudioClip source;
}

public class SoundManager : MonoBehaviour
{

    public List<MyAudio> ambientMusics = new List<MyAudio>();
    public List<MyAudio> soundEffects = new List<MyAudio>();

    public MyAudio currentMusic
    {
        get
        {
            return _currentMusic;
        }
        set
        {
            _currentMusic = value;
            PlayMusic(value.id);
        }
    }
    private MyAudio _currentMusic;

    public void PlayMusic(string id)
    {

    }

    public void PlaySound(string id)
    {

    }

    void Start()
    {

    }
}