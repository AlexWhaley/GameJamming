using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TrackViewModel : MonoBehaviour
{
    private AudioTrack _trackData;
    private PlayerInputHandler _input;
    [SerializeField] private PlayerCharacter _attachedCharacter;
    [SerializeField] private LaneViewModel _leftLane;
    [SerializeField] private LaneViewModel _rightLane;

    private void Start()
    {
        _input = InputManager.Instance.GetPlayerInputHandler(_attachedCharacter);
    }

    public void Initialize(AudioTrack track)
    {
        _trackData = track;
        _leftLane.Initialize(_attachedCharacter, Lane.Left, _trackData.LeftLane);
        _rightLane.Initialize(_attachedCharacter, Lane.Right, _trackData.RightLane);
    }

    private void Update()
    {
        UpdateLaneInput(_leftLane, _input.LeftStick);
        UpdateLaneInput(_rightLane, _input.RightStick);
    }

    private void UpdateLaneInput(LaneViewModel lane, PlayerInputHandler.AnalogueStick analogueStick)
    {
        if (lane.NotesInCatcher != null && lane.HeldNotes != null)
        {
            var catcherNote = lane.NotesInCatcher.NullSafePeek();
            var heldNote = lane.HeldNotes.NullSafePeek();

            if (catcherNote != null)
            {
                bool correctHit = analogueStick.Direction == catcherNote.Direction;
                bool correctHold = heldNote != null ? analogueStick.Direction == heldNote.Direction : false;

                if (!catcherNote.HasBeenHit && 
                    (analogueStick.IsPressed || (analogueStick.IsHeld && correctHit && heldNote != null && !correctHold)))
                {
                    catcherNote.HitNote(correctHit);
                    Debug.Log("Attempted note hit");
                }
            }

            if (heldNote != null)
            {
                bool correctHold = analogueStick.Direction == heldNote.Direction;

                if (analogueStick.IsReleased || (!heldNote.InSafeZone && !correctHold))
                {
                    heldNote.ReleaseHeldNote(false);
                }
            }
        }
    }
}