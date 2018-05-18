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
            TrackManager.Instance.AddLoopToQueue("play1a");
            TrackManager.Instance.PlayNextLoop();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            TrackManager.Instance.AddLoopToQueue("play1a");
        }

        
    }
}
