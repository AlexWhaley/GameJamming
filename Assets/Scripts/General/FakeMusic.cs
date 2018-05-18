using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMusic : MonoBehaviour
{
    [SerializeField]
    private GameObject _rhythmIndicator;
    private Coroutine _musicRoutine; 

    private void Start()
    {
        PhaseManager.Instance.IntroFinished += StartMusicLoop;
        PhaseManager.Instance.EndPhaseStarted += StopMusicLoop;

        PhaseManager.Instance.RhythmSegmentStarted += () => InidicateRhythm(true);
        PhaseManager.Instance.RhythmSegmentFinished += () => InidicateRhythm(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PhaseManager.Instance.NextPhase();
        }
    }

    public void StartMusicLoop()
    {
        _musicRoutine = StartCoroutine(MusicLoop());
    }

    public void StopMusicLoop()
    {
        StopCoroutine(_musicRoutine);
    }

    private void InidicateRhythm(bool isActive)
    {
        _rhythmIndicator.SetActive(isActive);
    }

    public IEnumerator MusicLoop()
    {
        Debug.Log("Intro Music Playing.");
        yield return new WaitForSeconds(10.0f);
        PhaseManager.Instance.NextPhase();

        while (true)
        {
            Debug.Log("Player Music Playing.");
            yield return new WaitForSeconds(10.0f);
            PhaseManager.Instance.NextPhase();
            Debug.Log("Enemy Music Playing.");
            yield return new WaitForSeconds(10.0f);
            PhaseManager.Instance.NextPhase();
        }
        
    }
}
