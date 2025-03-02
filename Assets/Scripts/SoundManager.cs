using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource MySource;

    public AudioClip MainMenuAudio;
    public AudioClip DayAudio;
    public AudioClip NightAudio;
    public AudioClip UnlockAudio;
    public AudioClip MorningQueue;
    public AudioClip NightQueue;
}
