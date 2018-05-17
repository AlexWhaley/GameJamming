using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TrackViewModel : MonoBehaviour
{
    private AudioTrack _trackData;
    [SerializeField] private PlayerCharacter _attachedCharacter;

    [SerializeField] private RectTransform _leftLane;
    [SerializeField] private RectTransform _rightLane;

    [SerializeField] private RectTransform _leftLaneSpawn;
    [SerializeField] private RectTransform _leftLaneDestruct;

    [SerializeField] private RectTransform _rightLaneSpawn;
    [SerializeField] private RectTransform _rightLaneDestruct;

    private int _nextLeftIndex = 0;
    private int _nextRightIndex = 0;

    public void Initialize(AudioTrack track)
    {
        _trackData = track;
    }

    private void Update()
    {
        if (TrackManager.Instance.PlayingTrack && _trackData != null)
        {
            UpdateTrackNotes(Lane.Left);
            UpdateTrackNotes(Lane.Right);
        }
    }

    private void UpdateTrackNotes(Lane lane)
    {
        RectTransform laneTransform = lane == Lane.Left ? _leftLane : _rightLane;
        Vector2 spawn = lane == Lane.Left ? _leftLaneSpawn.position : _rightLaneSpawn.position;
        Vector2 destruct = lane == Lane.Left ? _leftLaneDestruct.position : _rightLaneDestruct.position;
        int nextIndex = lane == Lane.Left ? _nextLeftIndex : _nextRightIndex;

        Note nextNote = GetNextNote(lane, nextIndex);
        if (nextIndex < GetNoteCount(lane) && nextNote.StartTime < AudioManager.Instance.SongPosition + TrackManager.Instance.BeatsShownInAdvance)
        {
            TrackManager.Instance.SpawnNote(_attachedCharacter, lane, laneTransform, nextNote, spawn, destruct);

            if (lane == Lane.Left)
            {
                _nextLeftIndex++;
            }
            else
            {
                _nextRightIndex++;
            }
        }
    }

    private int GetNoteCount(Lane lane)
    {
        int count = 0;

        List<NoteGroup> noteGroups = lane == Lane.Left ? _trackData.LeftLane : _trackData.RightLane;

        foreach (var group in noteGroups)
        {
            count += group.NoteChain.Count;
        }

        return count;
    }

    private Note GetNextNote(Lane lane, int nextIndex)
    {
        List<NoteGroup> noteGroups = lane == Lane.Left ? _trackData.LeftLane : _trackData.RightLane;

        int iterateIndex = 0;

        foreach(var group in noteGroups)
        {
            foreach(var note in group.NoteChain)
            {
                if (iterateIndex == nextIndex)
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
