using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyActionManager : MonoBehaviour
{
    [SerializeField]
    List<EnemyCharacter> _enemyCharactersInExecutionOrder;

    private static EnemyActionManager _instance;
    public static EnemyActionManager Instance
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
        }
    }

    private void Start()
    {
        PhaseManager.Instance.ActionSelectionAndEnemyAttacksStarted += StartEnemyTurn;
    }


    public void StartEnemyTurn()
    {
        List<SubmittedAction> enemySubmittedActions = new List<SubmittedAction>();
        int actionIndex = PickActionIndex();
        foreach (var enemy in _enemyCharactersInExecutionOrder)
        {
            if (enemy.IsAlive)
            {
                enemySubmittedActions.Add(CreateSubmittedActionForEnemy(enemy.EnemyActions[actionIndex], enemy));
            }
        }
        if (enemySubmittedActions.Count != 0)
        {
            BattleManager.Instance.ExecuteEnemyTurn(enemySubmittedActions);
        }
        else
        {
            //End game??!?!
        }
    }


    private SubmittedAction CreateSubmittedActionForEnemy(EnemyAction enemyAction, Character enemyCharacter)
    {
        List<ExecuteableAction> executeableActions = new List<ExecuteableAction>();

        foreach (var tAction in enemyAction.TargetedActions)
        {
            ExecuteableAction executeableAction = new ExecuteableAction(enemyCharacter, tAction);
            var potentialTargets = FindPotentialActionTargets(tAction, enemyCharacter);
            var finalTargets = SelectFinalTargets(tAction, potentialTargets, enemyCharacter);
            executeableAction.Targets = finalTargets;

            executeableActions.Add(executeableAction);
        }

        return new SubmittedAction(enemyCharacter, executeableActions);
    }

    private List<Character> FindPotentialActionTargets(EnemyTargetedAction targetedAction, Character targettingCharacter)
    {
        List<Character> potentialTargets = new List<Character>();
        switch (targetedAction.TargetType)
        {
            case TargetType.AllEnemiesExclusive:
                potentialTargets = CharacterManager.Instance.GetAliveEnemies();
                break;
            case TargetType.AllEnemiesInclusive:
                potentialTargets = CharacterManager.Instance.GetAliveEnemies();
                break;
            case TargetType.AllPlayersExclusive:
                potentialTargets = CharacterManager.Instance.GetAlivePlayers();
                break;
            case TargetType.AllPlayersInclusive:
                potentialTargets = CharacterManager.Instance.GetAlivePlayers();
                break;
            case TargetType.EnemySingle:
                potentialTargets = CharacterManager.Instance.GetAliveEnemies();
                break;
            case TargetType.PlayersSingle:
                potentialTargets = CharacterManager.Instance.GetAlivePlayers();
                break;
            case TargetType.Self:
                potentialTargets.Add(targettingCharacter);
                break;
        }
        return potentialTargets;
    }

    private List<Character> SelectFinalTargets(EnemyTargetedAction targetedAction, List<Character> potentialTargets, Character targettingCharacter)
    {
        List<Character> finalTargets = new List<Character>();
        Character targetPreference;
        switch (targetedAction.TargetType)
        {
            case TargetType.AllEnemiesExclusive:
            case TargetType.AllEnemiesInclusive:
            case TargetType.AllPlayersExclusive:
            case TargetType.AllPlayersInclusive:
                finalTargets = potentialTargets;
                break;
            case TargetType.EnemySingle:
                targetPreference = potentialTargets.FirstOrDefault(x => x.Name == targetedAction.TargetPreferenceName);
                if (targetPreference != null)
                {
                    finalTargets.Add(targetPreference);
                }
                else if (targetedAction.TargetLowest)
                {
                    finalTargets.Add(CharacterManager.Instance.GetWeakestEnemy());
                }
                else
                { 
                    finalTargets.Add(CharacterManager.Instance.GetRandomAliveEnemy());
                }
                break;
            case TargetType.PlayersSingle:
                targetPreference = potentialTargets.FirstOrDefault(x => x.Name == targetedAction.TargetPreferenceName);
                if (targetPreference != null)
                {
                    finalTargets.Add(targetPreference);
                }
                else if (targetedAction.TargetLowest)
                {
                    finalTargets.Add(CharacterManager.Instance.GetWeakestPlayer());
                }
                else
                {
                    finalTargets.Add(CharacterManager.Instance.GetRandomAlivePlayer());
                }
                break;
            case TargetType.Self:
                potentialTargets.Add(targettingCharacter);
                break;
        }
        return finalTargets;
    }

    private int PickActionIndex()
    {
        EnemyCharacter hench1 = _enemyCharactersInExecutionOrder[0];
        EnemyCharacter hench2 = _enemyCharactersInExecutionOrder[1];
        EnemyCharacter boss = _enemyCharactersInExecutionOrder[2];

        if (hench1.HealthPortionRemaining < 0.3 && hench1.IsAlive)
        {
            return 4;
        }
        else if (hench2.HealthPortionRemaining < 0.3 && hench2.IsAlive)
        {
            return 5;
        }
        else if (!hench1.IsAlive && !hench2.IsAlive)
        {
            return Random.Range(6, 8);
        }
        else
        {
            return Random.Range(0, 4);
        }
    }
}
