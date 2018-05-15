using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TrackViewModel : MonoBehaviour
{
    private AudioTrack _trackData;

    public void Initialize(AudioTrack track)
    {
        _trackData = track;
        _trackData = TrackManager.Instance.GetTrackFromId("demoTrack");
    }

    public void BuildTrack()
    {
        Initialize(null);

        // Build left lane
        TrackManager.Instance.BuildTrack(_trackData.LeftLane, Lane.Left, transform);

        // Build right lane
    }
}
