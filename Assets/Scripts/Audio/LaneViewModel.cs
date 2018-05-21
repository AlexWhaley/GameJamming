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
    
    public List<Note> _notes;

    private PlayerCharacter _attachedCharacter;
    private Lane _lane;
    private int _nextIndex = 0;

    public Queue<NoteViewModel> NotesInCatcher;
    public Queue<NoteViewModel> HeldNotes;

    public int NotesHit;

    public void Initialize(PlayerCharacter attachedCharacter, Lane lane, List<Note> notes)
    {
        _attachedCharacter = attachedCharacter;
        _lane = lane;
        _notes = notes;

        _nextIndex = 0;

        NotesHit = 0;

        NotesInCatcher = new Queue<NoteViewModel>();
        HeldNotes = new Queue<NoteViewModel>();
    }

    private void Update()
    {
        if (TrackManager.Instance.PlayingTrack && _notes != null)
        {
            UpdateTrackNotes();
        }
    }

    private void UpdateTrackNotes()
    {
        Note nextNote = GetNextNote();
        if (_nextIndex < GetNoteCount() && nextNote.StartTime < AudioManager.Instance.SongPosition + TrackManager.Instance.BeatsShownInAdvance/* && nextNote.ShouldSpawn*/)
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
        return _notes.Count;
    }

    private Note GetNextNote()
    {
        if (_nextIndex < _notes.Count)
        {
            return _notes[_nextIndex];
        }

        return null;
    }
}
