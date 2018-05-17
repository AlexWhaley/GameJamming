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

    public bool PlayingTrack = false;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnNote(PlayerID playerID, Lane lane, RectTransform trackTransform, Note note, Vector2 spawn, Vector2 destruct)
    {
        GameObject noteGO = Instantiate(AssetManager.Instance.NotePrefab, trackTransform);
        noteGO.GetComponent<NoteViewModel>().Initialize(playerID, note, lane, spawn, destruct);
    }

    public AudioTrack GetTrackFromId(string trackId)
    {
        return _audioTracks.FirstOrDefault(x => x.TrackId == trackId);
    }
}
