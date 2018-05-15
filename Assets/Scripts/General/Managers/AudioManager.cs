using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource _audioSource;

    [SerializeField] private List<AudioAsset> _audioClips;

    public bool IsPlaying { get; private set; }

    private float _songPosition;
    private float _audioStart;
    private float _secondsPerBeat;
    private int _songBeats;


    public TextMeshProUGUI _beatText;


    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    private void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaySound("demo");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            StopPlaying();
        }

        if (IsPlaying)
        {
            _songPosition = (float) AudioSettings.dspTime - _audioStart;
            var previousBeat = _songBeats;
            _songBeats = Mathf.CeilToInt(_songPosition / _secondsPerBeat);

            if (previousBeat != _songBeats)
            {
                _beatText.text = _songBeats.ToString();
            }
        }
    }

    public void PlaySound(string clipId)
    {
        var clip = GetAudioAssetFromId(clipId);

        if (clip != null)
        {
            _audioSource.clip = clip.Clip;
            _audioSource.Play();

            _audioStart = (float)AudioSettings.dspTime;
            _secondsPerBeat = 60f / clip.BeatsPerMinute;

            IsPlaying = true;
        }
    }

    public void StopPlaying()
    {
        _audioSource.Stop();
        IsPlaying = false;
    }

    private AudioAsset GetAudioAssetFromId(string audioId)
    {
        return _audioClips.FirstOrDefault(x => x.ClipId == audioId);
    }
}

[Serializable]
public class AudioAsset
{
    public string ClipId;
    public AudioClip Clip;
    public int BeatsPerMinute;
}
