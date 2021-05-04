using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public AudioSource[] playlist;

    public int songIndex = 0;

    public void nextSong(){
        playlist[songIndex].Stop(); 
        songIndex = (songIndex + 1) % playlist.Length;
        
        playlist[songIndex].Play();
    }

    public void previousSong(){
        playlist[songIndex].Stop();
        songIndex--;

        if(songIndex < 0){
            songIndex += playlist.Length;
        }

        playlist[songIndex].Play();
    }

    // void Awake(){
    //     foreach(Music m in music)
    //     {
    //         m.source = gameObject.AddComponent<AudioSource>(); 
    //         m.source.clip = m.clip;

    //         // m.source.volume = m.volume;
    //         // m.source.pitch = m.pitch;
            
    //     }
    // }

    // public void Play(string name)
    // {
    //     Music m = Array.Find(music, song => song.name == name);
    //     m.source.Play();
    // }

    // void Start(){
    //     Play("My Innermost Apocalypse");
    // }
    //}
    // Start is called before the first frame update

    // public static AudioClip ambiance;
    // public static AudioClip titleScreen;
    // static AudioSource audioSrc;
    // void Start()
    // {
    //     titleScreen = Resources.Load<AudioClip> ("TitleScreenMusic");
    //     ambiance = Resources.Load<AudioClip> ("My Innermost Apocalypse");
    //     audioSrc = GetComponent <AudioSource> ();
    //     PlayAmbiance();
    //     //PlayTitleMusic();
    // }

    // public static void PlayAmbiance()
    // {
    //     audioSrc.Stop();
    //     audioSrc.clip = ambiance;
    //     audioSrc.Play();
    // }

    // public static void PlayTitleMusic()
    // {
    //     audioSrc.Stop();
    //     audioSrc.clip = titleScreen;
    //     audioSrc.Play();
    // }
}
