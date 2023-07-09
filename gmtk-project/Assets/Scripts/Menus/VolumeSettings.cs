using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider mySlider;

    private void Start() {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume() {
        float volume = mySlider.value;
        myMixer.SetFloat("master", Mathf.Log10(volume)*20);
    }
    public void SetMusicVolume() {
        float volume = mySlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
    public void SetSFXVolume() {
        float volume = mySlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
    }
}
