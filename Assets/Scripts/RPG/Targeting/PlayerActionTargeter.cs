using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionTargeter : MonoBehaviour
{
    // Refactor this so one object contains all this UI related stuff together.
    [SerializeField]
    ActionMenuController _actionMenuController;

    private List<TargetedAction> _actionsToTarget;
    private PlayerCharacter _attatchedPlayer;

    private void Awake()
    {
        _attatchedPlayer = _actionMenuController.AttatchedPlayer;
    }

    public void InitiateTargettingSequence(Action action)
    {
        _actionsToTarget = action.TargetedActions;
    }

    private void ProcessTargetedAction(TargetedAction targetedAction)
    {
        // First find all potential targets
        List<Character> potentialTargets = FindPotentialActionTargets(targetedAction);
    }

    private List<Character> FindPotentialActionTargets(TargetedAction targetedAction)
    {
        List<Character> potentialTargets = new List<Character>();
        switch (targetedAction.TargetType)
        {
            case TargetType.AllEnemiesExclusive:
                potentialTargets = CharacterManager.Instance.GetNonTargetedAliveEnemies(_attatchedPlayer);
                break;
            case TargetType.AllEnemiesInclusive:
                potentialTargets = CharacterManager.Instance.GetAliveEnemies();
                break;
            case TargetType.AllPlayersExclusive:
                potentialTargets = CharacterManager.Instance.GetNonTargetedAlivePlayers(_attatchedPlayer);
                break;
            case TargetType.AllPlayersInclusive:
                potentialTargets = CharacterManager.Instance.GetAlivePlayers();
                break;
            case TargetType.EnemySingle:
                potentialTargets = CharacterManager.Instance.GetNonTargetedAliveEnemies(_attatchedPlayer);
                break;
            case TargetType.PlayersSingle:
                potentialTargets = CharacterManager.Instance.GetNonTargetedAlivePlayers(_attatchedPlayer);
                break;
            case TargetType.Self:
                potentialTargets.Add(_attatchedPlayer);
                break;
        }
        return potentialTargets;
    }
}

public class ExecuteableActions
{
    public List<Character> targets;
    Action actionToApply;
}
