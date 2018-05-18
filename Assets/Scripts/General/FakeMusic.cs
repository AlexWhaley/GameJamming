using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMusic : MonoBehaviour
{
    [SerializeField]
    private GameObject _rhythmCanvas;
    private Coroutine _musicRoutine; 

    private void Start()
    {
        
        PhaseManager.Instance.IntroFinished += () => AudioManager.Instance.PlaySound("choice0");
        PhaseManager.Instance.EndPhaseStarted += StopMusicLoop;

        PhaseManager.Instance.IntroActionSelectionStarted += TrackManager.Instance.QueueUpNextPlayerLoop;
        PhaseManager.Instance.ActionSelectionAndEnemyAttacksStarted += TrackManager.Instance.QueueUpNextPlayerLoop;

        PhaseManager.Instance.RhythmSegmentStarted += () => InidicateRhythm(true);
        PhaseManager.Instance.RhythmSegmentFinished += () => InidicateRhythm(false);

        AudioManager.Instance.SoundFinished += () => PhaseManager.Instance.NextPhase();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PhaseManager.Instance.NextPhase();
        }
    }

    public void StopMusicLoop()
    {
        StopCoroutine(_musicRoutine);
    }

    private void InidicateRhythm(bool isActive)
    {
        _rhythmCanvas.SetActive(isActive);
        if (isActive)
        {
            TrackManager.Instance.PlayNextLoop();
        }
        else
        {
            AudioManager.Instance.PlaySound("choice1");
        }
    }
}
