using UnityEngine;
using DG.Tweening;

public class AudioPlayer : MonoBehaviour
{

    public static AudioPlayer instance;

    public AudioSource backGroundPlayer;
    public  AudioSource VFXPlayer;

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

        VFXPlayer.clip = clip;

        VFXPlayer.Play();
    }
    public void PlayBackgroundMusic(AudioClip clip)
    {
        currentBackground = clip;     
        backGroundPlayer.clip = clip;
        backGroundPlayer.Play();
    }
}
