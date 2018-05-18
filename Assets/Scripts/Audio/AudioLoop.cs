using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class AudioLoop
{
    public string LoopId;
    public string AudioAssetId;

    public List<AudioTrack> Tracks;
    public int BeatsPerMinute;
}