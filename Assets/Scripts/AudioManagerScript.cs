using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(instance);
        PlayMusic();
    }
    public void PlayMusic()
    {
        musicSource.Play();
    }
    public void ToggleMuteMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
    public void ToggleMuteSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
}
