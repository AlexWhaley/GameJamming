using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<TrackViewModel> _playerTracks; 

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.PlaySound("demo");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.Instance.StopPlaying();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach(var track in _playerTracks)
            {
                track.Initialize(TrackManager.Instance.GetTrackFromId("demoTrack"));
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var track = GetPlayerTrack(PlayerID.Player1);
            track.BuildTrack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var track = GetPlayerTrack(PlayerID.Player2);
            track.BuildTrack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var track = GetPlayerTrack(PlayerID.Player3);
            track.BuildTrack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            var track = GetPlayerTrack(PlayerID.Player4);
            track.BuildTrack();
        }
    }

    private TrackViewModel GetPlayerTrack(PlayerID playerId)
    {
        return _playerTracks[(int)playerId];
    }
}
