using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum Lane
{
    Left,
    Right
}

public class LaneViewModel : MonoBehaviour
{
    [SerializeField] private RectTransform _noteStreamParent;
    [SerializeField] private RectTransform _spawn;
    [SerializeField] private RectTransform _destruct;
    
    public List<NoteGroup> _noteGroups;

    private PlayerCharacter _attachedCharacter;
    private Lane _lane;
    private int _nextIndex = 0;

    public Queue<NoteViewModel> NotesInCatcher;
    public Queue<NoteViewModel> HeldNotes;

    public void Initialize(PlayerCharacter attachedCharacter, Lane lane, List<NoteGroup> noteGroups)
    {
        _attachedCharacter = attachedCharacter;
        _lane = lane;
        _noteGroups = noteGroups;

        NotesInCatcher = new Queue<NoteViewModel>();
        HeldNotes = new Queue<NoteViewModel>();
    }

    private void Update()
    {
        if (TrackManager.Instance.PlayingTrack && _noteGroups != null)
        UpdateTrackNotes();
    }

    private void UpdateTrackNotes()
    {
        Note nextNote = GetNextNote();
        if (_nextIndex < GetNoteCount() && nextNote.StartTime < AudioManager.Instance.SongPosition + TrackManager.Instance.BeatsShownInAdvance)
        {
            SpawnNote(nextNote);

            _nextIndex++;
        }
    }

    private void SpawnNote(Note note)
    {
        GameObject noteGO = Instantiate(AssetManager.Instance.NotePrefab, _noteStreamParent);
        noteGO.GetComponent<NoteViewModel>().Initialize(_attachedCharacter, this, note, _lane, _spawn.position, _destruct.position);
    }

    private int GetNoteCount()
    {
        int count = 0;

        foreach (var group in _noteGroups)
        {
            count += group.NoteChain.Count;
        }

        return count;
    }

    private Note GetNextNote()
    {
        int iterateIndex = 0;

        foreach (var group in _noteGroups)
        {
            foreach (var note in group.NoteChain)
            {
                if (iterateIndex == _nextIndex)
                {
                    return note;
                }
                else
                {
                    iterateIndex++;
                }
            }
        }

        return null;
    }
}
