using UnityEngine;

public class AudioSender : MonoBehaviour
{
    [Header("“Ù∆µ∆¨∂Œ")]
    public AudioClip clip;
 

    public bool isBgm=false;
    [TextArea]
    public string description;
    private void OnEnable()
    {
        if(isBgm)  Play(clip);
    }
    private void OnDisable()
    {
        //AudioPlayer.instance.StopPlay();
    }
    public void Play(AudioClip clip)
    {
        if (isBgm)
            AudioPlayer.instance.PlayBackgroundMusic(clip);
        else AudioPlayer.instance.PlayVFX(clip);
    }
}
