using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip ambiance;
    public static AudioClip titleScreen;
    static AudioSource audioSrc;
    void Start()
    {
        titleScreen = Resources.Load<AudioClip> ("TitleScreenMusic");
        ambiance = Resources.Load<AudioClip> ("My_Innermost_Apocalypse");
        audioSrc = GetComponent <AudioSource> ();
        //PlayTitleMusic();
    }

    public static void PlayAmbiance()
    {
        audioSrc.Stop();
        audioSrc.clip = ambiance;
        audioSrc.Play();
    }

    public static void PlayTitleMusic()
    {
        audioSrc.Stop();
        audioSrc.clip = titleScreen;
        audioSrc.Play();
    }
}
