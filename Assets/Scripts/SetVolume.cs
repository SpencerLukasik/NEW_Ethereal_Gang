using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{  
    public AudioMixer mixer;

    void Start()
    {
        transform.GetComponent<Slider>().value = .1f;
    }

    public void setVolumeLevel(float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
}