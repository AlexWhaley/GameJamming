using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public float YScale = 120f;
    public static TrackManager Instance;
    
    [SerializeField] private List<AudioTrack> _audioTracks;

    [SerializeField] private List<RectTransform> _movingTracks;

    public bool PlayingTrack = false;

    private void Awake()
    {
        Instance = this;
    }

    public void BuildTrack(PlayerID playerID, List<NoteGroup> noteGroups, Lane lane, Transform trackTransform)
    {
        foreach (var noteGroup in noteGroups)
        {
            foreach (var note in noteGroup.NoteChain)
            {
                GameObject noteGO = Instantiate(AssetManager.Instance.NotePrefab, trackTransform);
                noteGO.transform.localPosition = new Vector3(0f, (note.StartTime - 1) * YScale, 0f);
                noteGO.GetComponent<NoteViewModel>().Initialize(playerID, note, lane);
            }
        }
    }

    private void Update()
    {
        if (PlayingTrack)
        {
            foreach (var track in _movingTracks)
            {
                track.anchoredPosition = new Vector2(track.localPosition.x, /*(YScale * 4)*/ -AudioManager.Instance.SongPosition * YScale);
            }
        }
    }

    public AudioTrack GetTrackFromId(string trackId)
    {
        return _audioTracks.FirstOrDefault(x => x.TrackId == trackId);
    }
}
