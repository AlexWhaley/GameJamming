using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool Player1LeftPressed;
    public bool Player1UpPressed;
    public bool Player1RightPressed;
    public bool Player1DownPressed;

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

        Player1LeftPressed = Input.GetKeyDown(KeyCode.A);
        Player1UpPressed = Input.GetKeyDown(KeyCode.W);
        Player1RightPressed = Input.GetKeyDown(KeyCode.D);
        Player1DownPressed = Input.GetKeyDown(KeyCode.S);
    }
}
