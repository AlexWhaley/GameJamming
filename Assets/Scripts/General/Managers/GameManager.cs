using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TrackManager.Instance.PlayTrack("demoTrack");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            AudioManager.Instance.AddSoundToQueue("play1");
            AudioManager.Instance.AddSoundToQueue("choice1");
            AudioManager.Instance.PlayNextQueueClip();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.Instance.StopPlaying();
        }
    }
}
