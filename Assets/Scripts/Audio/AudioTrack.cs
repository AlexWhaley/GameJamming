using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class AudioTrack
{
    public List<Note> LeftLane;
    public List<Note> RightLane;
}

[Serializable]
public class Note
{
    private Direction _direction;
    private static Random _rand;

    public float StartTime;
    public float Duration;
    public Direction Direction { get { return _direction; } }
    public bool LinkedToNextNote;
    public Note NextNote;

    public Note()
    {
        if (_rand == null)
        {
            _rand = new Random();
        }
        _direction = (Direction)_rand.Next(0, 4);
    }
    //[HideInInspector] public bool ShouldSpawn = true;
}

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}
