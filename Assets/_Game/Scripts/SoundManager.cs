using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    public AudioClip matchClip;
    public AudioClip failClip;
    public AudioClip flipClip;
  

    [Header("Audio Sources")]
    public AudioSource sfxSource;    // For sound effects (non-loop)
    public AudioSource musicSource;  // For background music (loop)

    [Header("UI Toggles")]
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private const string MUSIC_PREF = "MusicOn";
    private const string SFX_PREF = "SfxOn";

  

    void Start()
    {
        bool musicOn = PlayerPrefs.GetInt(MUSIC_PREF, 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt(SFX_PREF, 1) == 1;

        musicToggle.SetIsOnWithoutNotify(musicOn);
        sfxToggle.SetIsOnWithoutNotify(sfxOn);

        musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        sfxToggle.onValueChanged.AddListener(OnSfxToggleChanged);

        musicSource.loop = true;
        musicSource.Stop();

        if (musicOn)
        {
            musicSource.Play();
        }
    }


    void OnMusicToggleChanged(bool isOn)
    {
        if (isOn) musicSource.Play();
        else musicSource.Pause();

        PlayerPrefs.SetInt(MUSIC_PREF, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnSfxToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(SFX_PREF, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Play sounds directly, checking sfx toggle first

    public void PlayMatchSound()
    {
        if (sfxToggle.isOn && matchClip != null)
            sfxSource.PlayOneShot(matchClip);
    }

    public void PlayFailSound()
    {
        if (sfxToggle.isOn && failClip != null)
            sfxSource.PlayOneShot(failClip);
    }

    public void PlayFlipSound()
    {
        if (sfxToggle.isOn && flipClip != null)
            sfxSource.PlayOneShot(flipClip);
    }
}
