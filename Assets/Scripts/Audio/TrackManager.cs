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

    [SerializeField] private List<AudioTrack> _audioTracks;

    [SerializeField] private List<TrackViewModel> _playerTracks;

    public bool PlayingTrack = false;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayTrack(string trackId)
    {
        AudioTrack audioTrack = GetTrackFromId(trackId);

        foreach (var playerTrack in _playerTracks)
        {
            playerTrack.Initialize(TrackManager.Instance.GetTrackFromId("demoTrack"));
        }

        AudioManager.Instance.AddSoundToQueue(audioTrack.AudioAssetId, true);
    }

    public void SpawnNote(PlayerCharacter attachedCharacter, Lane lane, RectTransform trackTransform, Note note, Vector2 spawn, Vector2 destruct)
    {
        GameObject noteGO = Instantiate(AssetManager.Instance.NotePrefab, trackTransform);
        noteGO.GetComponent<NoteViewModel>().Initialize(attachedCharacter, note, lane, spawn, destruct);
    }

    public AudioTrack GetTrackFromId(string trackId)
    {
        return _audioTracks.FirstOrDefault(x => x.TrackId == trackId);
    }
}
