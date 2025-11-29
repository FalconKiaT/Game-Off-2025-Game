using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundType { SFX, Music, All }

public class SoundManager : MonoBehaviour
{

    [SerializeField] List<SoundObject> sfxs;
    [SerializeField] List<SoundObject> music;
    Dictionary<SoundObject, AudioSource> currentlyPlaying;
    string currentName;

    public float sfxVolume {  get; private set; }
    public float musicVolume { get; private set; }
    public float totalVolume { get; private set; }

    public SoundManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void Update()
    {
        Dictionary<SoundObject, AudioSource> temp = new(currentlyPlaying);
        foreach (var sound in temp)
        {
            if (!sound.Value.isPlaying)
            {
                currentlyPlaying.Remove(sound.Key);
                Destroy(sound.Value.gameObject);
            }
        }
    }


    public void Play(string name, SoundType type)
    {
        SoundObject currentSound = type switch
        {
            SoundType.SFX => sfxs.Find(FindName),
            SoundType.Music => music.Find(FindName),
            SoundType.All => FindSound(name),
            _ => new SoundObject()
        };

        if (currentlyPlaying.ContainsKey(currentSound))
        {
            currentlyPlaying[currentSound].Play();
            return;
        }

        currentName = name;
        GameObject childAudio = new("AudioSource");
        childAudio.transform.parent = gameObject.transform;
        AudioSource audioSource = childAudio.AddComponent<AudioSource>();

        currentlyPlaying[currentSound] = audioSource;

        audioSource.clip = currentSound.clip;
        audioSource.loop = false;
        audioSource.volume = currentSound.GetVolume();
        audioSource.pitch = currentSound.GetPitch();

    }

    public bool Pause(string name, SoundType type)
    {
        SoundObject currentSound = type switch
        {
            SoundType.SFX => sfxs.Find(FindName),
            SoundType.Music => music.Find(FindName),
            SoundType.All => FindSound(name),
            _ => new SoundObject()
        };

        if (currentlyPlaying.ContainsKey(currentSound))
        {
            if (currentSound.loop) currentlyPlaying[currentSound].Play();
            else currentlyPlaying[currentSound].Pause();
            return true;
        }
        else return false;
    }

    public void ChangeSFXVolume(float newVolume)
    {
        ChangeVolume(SoundType.SFX, newVolume);
    }

    public void ChangeMusicVolume(float newVolume)
    {
        ChangeVolume(SoundType.Music, newVolume);
    }

    public void ChangeTotalVolume(float newVolume)
    {
        ChangeVolume(SoundType.All, newVolume);
    }

    private void ChangeVolume(SoundType type, float newAmount)
    {
        switch (type)
        {
            case SoundType.All:
                totalVolume = newAmount;
                foreach (SoundObject sound in sfxs)
                {
                    sound.SetVolume(sfxVolume/2 + totalVolume/2);
                    if (currentlyPlaying.ContainsKey(sound)) currentlyPlaying[sound].volume = sound.GetVolume();
                }
                foreach (SoundObject sound in music)
                {
                    sound.SetVolume(musicVolume/2 + totalVolume/2);
                    if (currentlyPlaying.ContainsKey(sound)) currentlyPlaying[sound].volume = sound.GetVolume();
                }
                break;
            case SoundType.SFX:
                sfxVolume = newAmount;
                foreach (SoundObject sound in sfxs)
                {
                    sound.SetVolume(sfxVolume/2 + totalVolume/2);
                    if (currentlyPlaying.ContainsKey(sound)) currentlyPlaying[sound].volume = sound.GetVolume();
                }
                break;
            case SoundType.Music:
                musicVolume = newAmount;
                foreach (SoundObject sound in music)
                {
                    sound.SetVolume(musicVolume/2 + totalVolume/2);
                    if (currentlyPlaying.ContainsKey(sound)) currentlyPlaying[sound].volume = sound.GetVolume();
                }
                break;
        }
    }

    private bool FindName(SoundObject so)
    {
        if (so.name == currentName)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private SoundObject FindSound(string name)
    {
        SoundObject soundObject = sfxs.Find(FindName);

        if (soundObject.IsUnityNull()) soundObject = music.Find(FindName);

        return soundObject;
    }
}

[Serializable]
struct SoundObject
{
    public string name;
    public AudioClip clip;
    public bool loop { get; private set; }
    [SerializeField] float volume;
    [SerializeField] float pitch;

    public void SetVolume(float newAmount)
    {
        volume = newAmount;
    }

    public void ChangePitch(float amount)
    {
        pitch += amount;
    }
    public float GetVolume() { return volume; }
    public float GetPitch() { return pitch; }
}
