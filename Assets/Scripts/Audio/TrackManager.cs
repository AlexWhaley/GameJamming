using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public static TrackManager Instance;
    
    public float YScale;
    public int BeatsShownInAdvance = 4;

    [SerializeField] private List<AudioLoop> _audioLoops;

    [SerializeField] private List<TrackViewModel> _playerTracks;

    public bool PlayingTrack = false;

    private Queue<AudioLoop> _playQueue;

    private System.Random _rng;

    private void Awake()
    {
        Instance = this;
        _playQueue = new Queue<AudioLoop>();
        _rng = new System.Random(DateTime.Now.Second);
    }

    public void QueueUpNextPlayerLoop()
    {
        int index = _rng.Next(0, _audioLoops.Count);

        AddLoopToQueue(_audioLoops[index].LoopId);
    }

    public void AddLoopToQueue(string loopId)
    {
        AudioLoop loop = GetLoopFromId(loopId);
        if (loop != null)
        {
            _playQueue.Enqueue(loop);
            Debug.Log("Added [" + loopId + "] to queue.");
        }
    }

    public void PlayNextLoop()
    {
        if (_playQueue.Any())
        {
            AudioLoop loop = _playQueue.Dequeue();

            for (int i = 0; i < _playerTracks.Count; i++)
            {
                _playerTracks[i].Initialize(loop.Tracks[i]); ;
            }

            AudioManager.Instance.PlaySound(loop.AudioAssetId, true);
            PlayingTrack = true;
        }
    }
    public AudioLoop GetLoopFromId(string loopId)
    {
        return _audioLoops.FirstOrDefault(x => x.LoopId == loopId);
    }
}
