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

    private Queue<string> _audioPlayQueue = new Queue<string>();


    public TextMeshProUGUI _beatText;


    private void Awake()
    {
        Instance = this;

        foreach(var clip in _audioClips)
        {
            var go = new GameObject(clip.ClipId + "_AudioSource", typeof(AudioSource));
            go.transform.SetParent(transform);
            clip.AudioSource = go.GetComponent<AudioSource>();
            clip.AudioSource.clip = clip.Clip;
        }
    }
    
    private void Update()
    {
        if (IsPlaying)
        {
            _songTimeInSeconds = (float)(AudioSettings.dspTime - _audioStart);
            SongPosition = (_songTimeInSeconds / _secondsPerBeat) + 1;
            SongBeats = Mathf.FloorToInt(SongPosition);

            _beatText.text = SongBeats.ToString();

            if (!_audioSource.isPlaying)
            {
                PlayNextQueueClip();
            }
        }
    }

    public void PlaySound(string clipId, bool countIn = false)
    {
        var clip = GetAudioAssetFromId(clipId);

        if (clip != null && clip.AudioSource != null)
        {
            _secondsPerBeat = 60f / clip.BeatsPerMinute;

            _audioStart = (float)AudioSettings.dspTime;

            _audioSource = clip.AudioSource;
            _audioSource.Play();

            IsPlaying = true;
            TrackManager.Instance.PlayingTrack = true;
        }
    }

    public void AddSoundToQueue(string clipId, bool play = false)
    {
        _audioPlayQueue.Enqueue(clipId);

        if (play)
        {
            PlayNextQueueClip();
        }
    }

    public void PlayNextQueueClip()
    {
        PlaySound(_audioPlayQueue.Dequeue());
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
    public AudioSource AudioSource;
}
