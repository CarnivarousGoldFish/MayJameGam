using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Slider musicSlider, sfxSlider;
    public Sound[] musicSounds,sfxSounds;
    public AudioSource musicSource,sfxSource;
    public float volumeScale;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        setVolume(1);
        PlayMusic("BGM");
    }

    public void setVolume(float scale){
        volumeScale=scale;
        Debug.Log("master volume: "+volumeScale);
        setMusicVolume(musicSlider.value);
        setSFXVolume(sfxSlider.value);
    }
    public void setMusicVolume(float volume){
        musicSource.volume=volume*volumeScale;
        Debug.Log("music volume: "+musicSource.volume);
    }
    public void setSFXVolume(float volume){
        sfxSource.volume=volume*volumeScale;
        Debug.Log("sfx volume: "+sfxSource.volume);
    }

    public void PlayMusic(string name){
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s!=null){
            musicSource.clip=s.clip;
            musicSource.Play();
        }else{
            Debug.Log("Audio Clip Not Found");
        }
    }

    public void PlaySFX(string name){
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if(s!=null){
            sfxSource.clip=s.clip;
            sfxSource.Play();
        }else{
            Debug.Log("Audio Clip Not Found");
        }
    }
}
