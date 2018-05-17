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

    private void Awake()
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
        var leftNote = _leftLane.NoteInCatcher;
        var rightNote = _rightLane.NoteInCatcher;
        
        if (_input != null && _input.LeftStick.IsHeld)
        {
            if (leftNote != null)
            {
                if (_input.LeftStick.Direction == leftNote.Direction)
                {
                    leftNote.HasBeenHit = true;
                    if (leftNote.HasTail)
                    {

                    }
                }
            }
        }

        if (_input != null && _input.RightStick.IsHeld)
        {
            if (rightNote != null)
            {
                if (_input.RightStick.Direction == rightNote.Direction)
                {
                    rightNote.HasBeenHit = true;
                    if (rightNote.HasTail)
                    {

                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            //if (leftNote)
        }
    }
}
