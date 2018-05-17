using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class AudioTrack
{
    public string TrackId;
    public string AudioAssetId;

    public List<NoteGroup> LeftLane;
    public List<NoteGroup> RightLane;
}

[Serializable]
public class NoteGroup
{
    public List<Note> NoteChain;
}

[Serializable]
public class Note
{
    public float StartTime;
    public float Duration;
    public Direction Direction;
}

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}
