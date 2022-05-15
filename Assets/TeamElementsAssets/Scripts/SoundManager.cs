using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;

    private void Awake()
    {
        if(singleton != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
        singleton = this;

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.volume = s.baseVolume;
            s.pitch = s.basePitch;
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
        }
    }

    private void Start()
    {
        Sound s = sounds[0];
        if(s != null)
        {
            currentMusic = s;
            Play(s.id);
        }
    }

    public Sound[] sounds;

    private Sound currentMusic;

    public float soundVolumeMultiplier
    {
        get
        {
            return _soundVolumeMultiplier;
        }
        set
        {
            _soundVolumeMultiplier = Mathf.Clamp(value, 0f, 2f);
            UpdateVolumes();
        }
    }
    private float _soundVolumeMultiplier;

    public void Play(string id)
    {
        Sound s = Array.Find(sounds, s => s.id == id);
        if(s != null)
        {
            s.source.Play();
        }
    }

    private Coroutine fadeCo;

    public void PlayWithFade(string id, float fadeTime = 1f)
    {
        if (fadeCo != null) return;
        Sound sound = Array.Find(sounds, s => s.id == id);
        if (sound == null || sound == currentMusic) return;
        fadeCo = StartCoroutine(PlayWithFadeCo(sound, fadeTime));
    }
    public IEnumerator PlayWithFadeCo(Sound sound, float fadeTime)
    {
        while(currentMusic.volume > Vector3.kEpsilon)
        {
            currentMusic.volume -= (Time.deltaTime / fadeTime);
            yield return new WaitForEndOfFrame();
        }
        currentMusic.source.Stop();
        currentMusic.source.volume = currentMusic.baseVolume;
        currentMusic = sound;
        currentMusic.volume = 0f;
        currentMusic.source.Play();
        while(currentMusic.volume < currentMusic.baseVolume)
        {
            currentMusic.volume += (Time.deltaTime / fadeTime);
            yield return new WaitForEndOfFrame();
        }
        fadeCo = null;
        yield return null;
    }

    public void UpdateVolumes()
    {
        foreach(Sound s in sounds)
        {
            s.volume = s.baseVolume * soundVolumeMultiplier;
        }
    }
}

[Serializable]
public class Sound
{
    public string id;
    public AudioClip clip
    {
        get
        {
            return _clip;
        }
        set
        {
            _clip = value;
            if (source != null) source.clip = clip;
        }
    }
    [HideInInspector]
    public float volume
    {
        get
        {
            if (source == null) return -1f;
            return source.volume;
        }
        set
        {
            if (source != null) source.volume = value;
        }
    }
    public float baseVolume
    {
        get
        {
            return _baseVolume;
        }
        set
        {
            _baseVolume = value;
        }
    }
    [HideInInspector]
    public float pitch
    {
        get
        {
            if (source == null) return -1f;
            return source.pitch;
        }
        set
        {
            if (source != null) source.pitch = value;
        }
    }
    public float basePitch
    {
        get
        {
            return _basePitch;
        }
        set
        {
            _basePitch = value;
        }
    }
    public bool loop
    {
        get
        {
            return _loop;
        }
        set
        {
            _loop = value;
            if (source != null) source.loop = loop;
        }
    }
    [HideInInspector]
    public AudioSource source;

    [SerializeField]
    private AudioClip _clip;
    [Range(0f, 1f), SerializeField]
    private float _baseVolume = 1f;
    [Range(.1f, 3f), SerializeField]
    private float _basePitch = 1f;
    [SerializeField]
    private bool _loop = true;
}