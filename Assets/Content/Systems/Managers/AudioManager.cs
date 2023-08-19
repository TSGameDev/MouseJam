using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource globalAudioSource;
    [SerializeField] private AudioSource globalMusicSource;
    [SerializeField] private AudioClip musicClip;

    [Header("Audio Mixer Settings")]
    [SerializeField] AudioMixer mainAudioMixer;
    [SerializeField] private string masterMixerName;
    [SerializeField] private string soundEffectMixerName;
    [SerializeField] private string musicMixerName;

    [SerializeField] private Slider masterMixerVolumeSlider;
    [SerializeField] private Slider soundEffectMixerVolumeSlider;
    [SerializeField] private Slider musicMixerVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterVolumeTxt;
    [SerializeField] private TextMeshProUGUI soundEffectVolumeTxt;
    [SerializeField] private TextMeshProUGUI musicVolumeTxt;

    public void SetGlobalMusicClip(AudioClip _MusicClip)
    {
        if (globalMusicSource == null)
            return;

        musicClip = _MusicClip;
        SetAndPlay(globalMusicSource, musicClip);
    }

    #region Setup

    private AudioManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        globalMusicSource.clip = musicClip;
        globalMusicSource.Play();
    }

    #endregion

    #region Audio Source Functions

    public void SetAndPlay(AudioSource _AudioSource, AudioClip _AudioClip)
    {
        _AudioSource.clip = _AudioClip;
        _AudioSource.Play();
    }

    public void PlayOneShot(AudioSource _AudioSource, AudioClip _AudioClip)
    {
        _AudioSource.PlayOneShot(_AudioClip);
    }

    public void PlayOneShot(AudioClip _AudioClip)
    {
        globalAudioSource.PlayOneShot(_AudioClip);
    }

    #endregion

    #region Audio Mixer Functions

    public void SetSoundValue(AudioMixer _AudioMixer, string _MixerValueName, float _MixerValue)
    {
        _AudioMixer.SetFloat(_MixerValueName, _MixerValue);
    }

    public void SetMasterVolumeMixer()
    {
        mainAudioMixer.SetFloat(masterMixerName, masterMixerVolumeSlider.value);
    }
    public void SetSoundEffectVolumeMixer()
    {
        mainAudioMixer.SetFloat(soundEffectMixerName, soundEffectMixerVolumeSlider.value);
    }

    public void SetMusicVolumeMixer()
    {
        mainAudioMixer.SetFloat(musicMixerName, musicMixerVolumeSlider.value);
    }

    #endregion
}
