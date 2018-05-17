using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _audioSource;

    [SerializeField] private List<AudioAsset> _audioClips;

    public bool IsPlaying { get; private set; }

    private float _songTimeInSeconds;
    private float _audioStart;
    private float _secondsPerBeat;

    public int SongBeats;
    public float SongPosition;


    public TextMeshProUGUI _beatText;


    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsPlaying)
        {
            _songTimeInSeconds = (float)(AudioSettings.dspTime - _audioStart);
            SongPosition = (_songTimeInSeconds / _secondsPerBeat) + 1;
            SongBeats = Mathf.FloorToInt(SongPosition);

            _beatText.text = SongBeats.ToString();
        }
    }

    public void PlaySound(string clipId)
    {
        var clip = GetAudioAssetFromId(clipId);

        if (clip != null)
        {
            _audioSource.clip = clip.Clip;
            _secondsPerBeat = 60f / clip.BeatsPerMinute;

            _audioStart = (float)AudioSettings.dspTime;
            _audioSource.Play();

            IsPlaying = true;
            TrackManager.Instance.PlayingTrack = true;
        }
    }

    public void StopPlaying()
    {
        _audioSource.Stop();
        IsPlaying = false;
        TrackManager.Instance.PlayingTrack = false;
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
