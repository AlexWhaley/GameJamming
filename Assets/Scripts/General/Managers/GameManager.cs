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
            foreach (var track in _playerTracks)
            {
                track.Initialize(TrackManager.Instance.GetTrackFromId("demoTrack"));
            }
            AudioManager.Instance.PlaySound("demo");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.Instance.StopPlaying();
        }
    }

    private TrackViewModel GetPlayerTrack(PlayerID playerId)
    {
        return _playerTracks[(int)playerId];
    }
}
