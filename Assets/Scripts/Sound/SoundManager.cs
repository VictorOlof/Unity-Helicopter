using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource, _effectsSource;
    public static SoundManager Instance;

    [SerializeField] private AudioClip _upSoundClip, _downSoundClip;

    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        LevelEvents.OnNewLevel += PlayLevelMusic;
    }

    private void OnDisable()
    {
        LevelEvents.OnNewLevel -= PlayLevelMusic;
    }

    private void PlayLevelMusic(LevelParameters levelParameters)
    {
        if (levelParameters.levelMusic && levelParameters.levelMusic != _musicSource.clip)
        {
            _musicSource.Stop();
            _musicSource.clip = levelParameters.levelMusic;
            _musicSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.Stop();
        _effectsSource.PlayOneShot(clip);
    }

    public void PlaySoundUp()
    {
        _effectsSource.Stop();
        _effectsSource.PlayOneShot(_upSoundClip);
    }

    public void PlaySoundDown()
    {
        _effectsSource.Stop();
        _effectsSource.PlayOneShot(_downSoundClip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

}
