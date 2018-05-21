using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class AudioTrack
{
    public List<Note> LeftLane;
    public List<Note> RightLane;
}

[Serializable]
public class Note
{
    public float StartTime;
    public float Duration;
    public Direction Direction;
    public bool LinkedToNextNote;
    public Note NextNote;
    //[HideInInspector] public bool ShouldSpawn = true;
}

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}
