using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour
{

    public static AudioPlayer instance;

    public AudioSource backGroundPlayer;
    public  List<AudioSource> VFXPlayers;

    private AudioClip currentBackground;
    private AudioClip curVFX;

    private void Awake()
    {
        if(instance==null)
        instance = this;
        else Destroy(instance);
    }

    private void Start()
    {
    }

    public void StopPlay()
    {
        //backGroundPlayer.Stop();
    }
    public void PlayVFX(AudioClip clip)
    {
        curVFX = clip;

        for (int i = 0; i < VFXPlayers.Count; i++)
        {
            if (!VFXPlayers[i].isPlaying)
            {
                VFXPlayers[i].clip = clip;
                VFXPlayers[i].Play();
            }
        }
    }
    public void PlayBackgroundMusic(AudioClip clip)
    {
        currentBackground = clip;     
        backGroundPlayer.clip = clip;
        backGroundPlayer.Play();
    }
}
