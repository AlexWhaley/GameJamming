using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public float YScale = 0.1f;
    public static TrackManager Instance;

    [SerializeField] private GameObject _notePrefab;


    [SerializeField] private List<AudioTrack> _audioTracks;

    private void Awake()
    {
        Instance = this;
    }

    public void BuildTrack(List<NoteGroup> noteGroups, Lane lane, Transform trackTransform)
    {
        foreach (var noteGroup in noteGroups)
        {
            foreach (var note in noteGroup.NoteChain)
            {
                GameObject noteGO = Instantiate(_notePrefab, trackTransform);
                noteGO.transform.localPosition = new Vector3(0f, (note.StartTime - 1) * YScale, 0f);
                noteGO.GetComponent<NoteViewModel>().Initialize(note, lane);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var track = FindObjectOfType<TrackViewModel>();
            track.BuildTrack();
        }
    }

    public AudioTrack GetTrackFromId(string trackId)
    {
        return _audioTracks.FirstOrDefault(x => x.TrackId == trackId);
    }
}
