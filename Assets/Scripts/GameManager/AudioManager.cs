using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    private static AudioManager am;

    public static void ChangeMusicVolume(float num)
    {
        FindAudioManager();
        am.ChangeMusicVolume(num);
    }

    public static void PlaySong(Song song)
    {
        FindAudioManager();
        am.PlaySong(song);
    }

    public static float GetMusicVolume()
    {
        FindAudioManager();
        return am.GetMusicVolume();
    }

    private static void FindAudioManager()
    {
        if (am == null) am = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // AudioHelper could not find AudioManager
        if (am == null) Debug.LogError("AudioHelper could not find 'AudioManager'");
    }
}

public enum Song
{
    faster_does_it, hep_cats, hot_swing, COUNT
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip faster_does_it;
    [SerializeField] private AudioClip hep_cats;
    [SerializeField] private AudioClip hot_swing;

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public void ChangeMusicVolume(float num)
    {
        if (num < 0f || num > 1f)
        {
            return;
        }
        musicSource.volume = num;
    }

    public void PlaySong(Song song)
    {   
        AudioClip newClip = GetClipFromEnum(song);
        if (newClip == musicSource.clip)
            return;
        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    private AudioClip GetClipFromEnum(Song song)
    {
        switch (song)
        {
            default:
            case Song.faster_does_it:
                return faster_does_it;
            case Song.hep_cats:
                return hep_cats;
            case Song.hot_swing:
                return hot_swing;
        }
    }
}
