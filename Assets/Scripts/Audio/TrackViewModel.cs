using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum PlayerID
{
    Player1,
    Player2,
    Player3,
    Player4
}

public class TrackViewModel : MonoBehaviour
{
    private AudioTrack _trackData;
    public PlayerID PlayerID;

    [SerializeField] private Transform _leftLane;
    [SerializeField] private Transform _rightLane;

    public void Initialize(AudioTrack track)
    {
        _trackData = track;
    }

    public void BuildTrack()
    {
        // Build left lane
        TrackManager.Instance.BuildTrack(_trackData.LeftLane, Lane.Left, _leftLane);

        // Build right lane
        TrackManager.Instance.BuildTrack(_trackData.LeftLane, Lane.Right, _rightLane);
    }
}
