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

public enum Lane
{
    Left,
    Right
}

public enum Direction
{
    Left = 0,
    Up = 1,
    Right = 2,
    Down = 3
}
