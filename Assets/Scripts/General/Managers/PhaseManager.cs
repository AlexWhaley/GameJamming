using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public enum GameflowPhases
    {
        Intro,
        IntroActionSelection,
        Rhythm,
        ActionExecution,
        ActionSelectionAndEnemyAttacks,
        End
    }

    private GameflowPhases _currentGamePhase;

    private static PhaseManager _instance;
    public static PhaseManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        // Wont need this just for clarification.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // If the singleton hasn't been initialized yet
            _instance = this;
            Initialise();
        }
    }

    private void Initialise()
    {
        _currentGamePhase = GameflowPhases.Intro;
        AudioManager.Instance.PlaySound("splash");
    }

    public void NextPhase()
    {
        switch (_currentGamePhase)
        {
            // Triggered by player music start
            case GameflowPhases.Intro:
            // Wait for intro music to stop then start thythm section with countdown?
                _currentGamePhase = GameflowPhases.IntroActionSelection;
                IntroFinished();
                IntroActionSelectionStarted();
                break;

            //Will be triggered by time running out in action selection.
            case GameflowPhases.IntroActionSelection:
                _currentGamePhase = GameflowPhases.Rhythm;
                IntroActionSelectionFinished();
                RhythmSegmentStarted();
                break;
            case GameflowPhases.ActionSelectionAndEnemyAttacks:
                _currentGamePhase = GameflowPhases.Rhythm;
                ActionSelectionAndEnemyAttacksFinished();
                RhythmSegmentStarted();
                break;

            //Triggered by enemy music starting
            case GameflowPhases.Rhythm:
                _currentGamePhase = GameflowPhases.ActionExecution;
                RhythmSegmentFinished();
                ActionExecutionStarted();
                break;

            //Triggered by animations ending
            case GameflowPhases.ActionExecution:
                ActionExecutionFinished();
                if (CharacterManager.Instance.GetAliveEnemies().Count != 0)
                {
                    _currentGamePhase = GameflowPhases.ActionSelectionAndEnemyAttacks;
                    ActionSelectionAndEnemyAttacksStarted();
                }
                else
                {
                    _currentGamePhase = GameflowPhases.End;
                    EndPhaseStarted();
                }
                break;

            case GameflowPhases.End:
                EndPhaseFinished();
                break;


        }
    }
    
    public delegate void PhaseDel();

    public PhaseDel IntroFinished = new PhaseDel(() => { } );
    public PhaseDel IntroActionSelectionStarted = new PhaseDel(() => { });
    public PhaseDel IntroActionSelectionFinished = new PhaseDel(() => { });
    public PhaseDel RhythmSegmentStarted = new PhaseDel(() => { });
    public PhaseDel RhythmSegmentFinished = new PhaseDel(() => { });
    public PhaseDel ActionExecutionStarted = new PhaseDel(() => { });
    public PhaseDel ActionExecutionFinished = new PhaseDel(() => { });
    public PhaseDel ActionSelectionAndEnemyAttacksStarted = new PhaseDel(() => { });
    public PhaseDel ActionSelectionAndEnemyAttacksFinished = new PhaseDel(() => { });
    public PhaseDel EndPhaseStarted = new PhaseDel(() => { });
    public PhaseDel EndPhaseFinished = new PhaseDel(() => { });
}

