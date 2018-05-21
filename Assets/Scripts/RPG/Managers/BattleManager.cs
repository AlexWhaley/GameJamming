using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private List<SubmittedAction> _submittedPlayerActions;
    private Coroutine _actionSequence;
    private static BattleManager _instance;

    [SerializeField] private Action _defaultAction;

    public static BattleManager Instance
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
        PhaseManager.Instance.ActionExecutionStarted += EndPlayersTurns;
    }

    public void StartPlayersTurns()
    {
        _submittedPlayerActions = new List<SubmittedAction>();
        
    }

    public void EndPlayersTurns()
    {
        RemovePlayerShields();

        List<Character> alivePlayers = CharacterManager.Instance.GetAlivePlayers();
        foreach (var player in alivePlayers)
        {
            if (_submittedPlayerActions.FirstOrDefault(x => x.TargetingCharacter == player) == null)
            {
                ExecuteableAction executeableAction = new ExecuteableAction(player, _defaultAction.TargetedActions[0]);
                executeableAction.Targets.Add(CharacterManager.Instance.GetRandomAliveEnemy());

                List<ExecuteableAction> defaultActions = new List<ExecuteableAction> {executeableAction};

                SubmittedAction submittedAction = new SubmittedAction(player, defaultActions);
                _submittedPlayerActions.Add(submittedAction);


            }
        }

        _actionSequence = StartCoroutine(ProcessSubmittedTurns(_submittedPlayerActions, false));
    }

    public void ExecuteEnemyTurn(List<SubmittedAction> enemyActions)
    {
        _actionSequence = StartCoroutine(ProcessSubmittedTurns(enemyActions, true));
    }

    private void RemovePlayerShields()
    {
        var players = CharacterManager.Instance.GetPlayers();
        foreach (var player in players)
        {
            player.ResetShieldModifier();
        }
    }

    private void RemoveEnemyShields()
    {
        var enemies = CharacterManager.Instance.GetEnemies();
        foreach (var enemy in enemies)
        {
            enemy.ResetShieldModifier();
        }
    }

    private IEnumerator ProcessSubmittedTurns(List<SubmittedAction> submittedActions, bool isEnemyTurn)
    {
        float actionTime = 0.5f;
        foreach (var submittedAction in submittedActions)
        {

            StartCoroutine(HandleSubmittedAction(submittedAction, actionTime));
            yield return new WaitForSeconds(actionTime);
        }
        if (!isEnemyTurn)
        {
            PhaseManager.Instance.NextPhase();
        }
    }

    private IEnumerator HandleSubmittedAction(SubmittedAction submittedAction, float actionTime)
    {
        int actionSegements = submittedAction.ExecuteableActions.Count;
        float segmentTime = actionTime / actionSegements;

        foreach (var executableAction in submittedAction.ExecuteableActions)
        {
            ExecuteableAction recalculatedAction = RecalculateTargets(executableAction);
            ExecuteAction(recalculatedAction);
            yield return new WaitForSeconds(segmentTime);
        }
    }

    private void ExecuteAction(ExecuteableAction executeableAction)
    {
        int targetNumber = executeableAction.Targets.Count;
        bool usedAttack = false;
        foreach (var target in executeableAction.Targets)
        {
            foreach (var actionComponent in executeableAction.ActionToApply.ActionComponents)
            {
                if (actionComponent is DamageComponent)
                {
                    usedAttack = true;
                }
                actionComponent.ExecuteAction(target, executeableAction.Targeter, 1.0f / targetNumber);
            }
        }
        if (usedAttack)
        {
            executeableAction.Targeter.ResetAttackBuffModifier();
        }
    }

    private ExecuteableAction RecalculateTargets(ExecuteableAction executableAction)
    {
        List<Character> newTargets = new List<Character>();
        foreach (var target in executableAction.Targets)
        {
            if (target.IsAlive)
            {
                newTargets.Add(target);

            }
            else
            {
                switch (executableAction.ActionToApply.TargetType)
                {
                    case TargetType.EnemySingle:
                        newTargets.Add(CharacterManager.Instance.GetRandomAliveEnemy());
                        break;
                    case TargetType.PlayersSingle:
                        newTargets.Add(CharacterManager.Instance.GetRandomAlivePlayer());
                        break;

                }
            }
        }

        executableAction.Targets = newTargets;
        return executableAction;
    }


    public void SubmitCharacterAction(Character playerCharacter, List<ExecuteableAction> lockedInActions)
    {
        SubmittedAction existingSubmission = _submittedPlayerActions.FirstOrDefault(x => x.TargetingCharacter == playerCharacter);
        if (existingSubmission != null)
        {
            _submittedPlayerActions.Remove(existingSubmission);
        }
        _submittedPlayerActions.Add(new SubmittedAction(playerCharacter, lockedInActions));
    }

    public void RemoveSubmittedActions(Character playerCharacter)
    {
        foreach (var submision in _submittedPlayerActions)
        {
            if (submision.TargetingCharacter == playerCharacter)
            {
                _submittedPlayerActions.Remove(submision);
            }
        }
    }
}

public class SubmittedAction
{
    public Character TargetingCharacter;
    public List<ExecuteableAction> ExecuteableActions;

    public SubmittedAction(Character targetingCharacter, List<ExecuteableAction> executeableActions)
    {
        TargetingCharacter = targetingCharacter;
        ExecuteableActions = executeableActions;
    }
}
