using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActionTargeter : MonoBehaviour, InputCommandHandler
{
    private PlayerUIController _uiController;

    private List<TargetedAction> _actionsToTarget;
    private PlayerCharacter _attatchedPlayer;

    private List<Character> _potentialTargets;
    private TargetType _currentTargetType;
    private int _currentSingleTargetIndex;

    private int _currentTargetedActionIndex;
    private List<ExecuteableAction> _confirmedActions;

    private bool _isRegistered = false;

    private enum TargetSelectionMode
    {
        Choice,
        NoChoice
    }

    private TargetSelectionMode _currentTargetSelectionMode;
    private void Awake()
    {
        _uiController = GetComponentInParent<PlayerUIController>();
        _attatchedPlayer = _uiController.AttatchedPlayer;

    }

    public void InitiateTargettingSequence(Action action)
    {
        _actionsToTarget = action.TargetedActions;
        _currentTargetedActionIndex = 0;
        _confirmedActions = new List<ExecuteableAction>();

        ProcessTargetedAction();
        RegisterInputHandlers();
    }

    private TargetedAction CurrentTargetedAction
    {
        get
        {
            return _actionsToTarget[_currentTargetedActionIndex];
        }
    }

    public void ReturnToActionMenuController()
    {
        UnregisterInputHandlers();
        _uiController.OpenActionMenu(false);
    }

    private void ProcessTargetedAction()
    {
        // First find all potential targets
        if (_currentTargetedActionIndex != _actionsToTarget.Count)
        {
            _currentTargetType = CurrentTargetedAction.TargetType;
            _potentialTargets = FindPotentialActionTargets(CurrentTargetedAction);

            DetermineCurrentTargetingMode();
            PlaceInitialTargettingIndicators();
        }
        else
        {
            _uiController.LockInActionTargets();
            BattleManager.Instance.SubmitCharacterAction(_attatchedPlayer, _confirmedActions);
        }

    }

    private void PlaceInitialTargettingIndicators()
    {
        switch (_currentTargetSelectionMode)
        {
            case TargetSelectionMode.Choice:
                _currentSingleTargetIndex = 0;
                SetTargetedIndex(_currentSingleTargetIndex, true);
                break;
            case TargetSelectionMode.NoChoice:
                foreach (var character in _potentialTargets)
                {
                    character.TargetIndicatorController.SetActiveTargetingIndicator(_attatchedPlayer, true);
                }
                break;
        }
    }

    public void RemoveAllIndicators()
    {
        if (_potentialTargets != null)
        {
            foreach (var character in _potentialTargets)
            {
                character.TargetIndicatorController.SetActiveTargetingIndicator(_attatchedPlayer, false);
            }
        }
    }

    private void MoveSingleTargetIndicator(int indexChange)
    {
        SetTargetedIndex(_currentSingleTargetIndex, false);
        
        int potentialTargetCount = _potentialTargets.Count;
        _currentSingleTargetIndex = (((_currentSingleTargetIndex + indexChange) % potentialTargetCount) + potentialTargetCount) % potentialTargetCount;
        SetTargetedIndex(_currentSingleTargetIndex, true);
    }

    private void RevokeLastTargetedAction()
    {
        // This logic is bad and wont work with long chains but we never will be targetting more than twice per player in a single action so it doesnt matter now.
        RemoveAllIndicators();
        if (_currentTargetedActionIndex != 0)
        {
            _uiController.UnlockInActionTargets();
            _confirmedActions.RemoveAt(--_currentTargetedActionIndex);
            ProcessTargetedAction();
        }
        else
        {
            ReturnToActionMenuController();
        }
    }

    private void LockInAction()
    {
        if (_currentTargetedActionIndex < _actionsToTarget.Count)
        {
            ExecuteableAction actionToLock = new ExecuteableAction(_attatchedPlayer, CurrentTargetedAction);
            switch (_currentTargetSelectionMode)
            {
                case TargetSelectionMode.Choice:
                    actionToLock.Targets.Add(_potentialTargets[_currentSingleTargetIndex]);
                    break;
                case TargetSelectionMode.NoChoice:
                    actionToLock.Targets = _potentialTargets.ToList();
                    break;
            }
            _confirmedActions.Add(actionToLock);

            //Process next targeted action or end.
            ++_currentTargetedActionIndex;
            ProcessTargetedAction();
        }
    }

    private void SetTargetedIndex(int index, bool setTargeted)
    {
        _potentialTargets[index].TargetIndicatorController.SetActiveTargetingIndicator(_attatchedPlayer, setTargeted);
    }


    private List<Character> FindPotentialActionTargets(TargetedAction targetedAction)
    {
        List<Character> potentialTargets = new List<Character>();
        switch (_currentTargetType)
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

    private void DetermineCurrentTargetingMode()
    {

        switch (_currentTargetType)
        {
            case TargetType.AllEnemiesExclusive:
            case TargetType.AllEnemiesInclusive:
            case TargetType.AllPlayersExclusive:
            case TargetType.AllPlayersInclusive:
            case TargetType.Self:
                _currentTargetSelectionMode = TargetSelectionMode.NoChoice;
                break;
            case TargetType.EnemySingle:
            case TargetType.PlayersSingle:
                _currentTargetSelectionMode = TargetSelectionMode.Choice;
                break;
        }
    }

    public void HandleBackButton()
    {
        RevokeLastTargetedAction();
    }

    public void HandleConfirmButton()
    {
        LockInAction();
    }

    public void HandleUpButton()
    {
    }

    public void HandleDownButton()
    {
    }

    public void HandleLeftButton()
    {
        if (_currentTargetSelectionMode == TargetSelectionMode.Choice)
        {
            MoveSingleTargetIndicator(-1);
        }
    }

    public void HandleRightButton()
    {
        if (_currentTargetSelectionMode == TargetSelectionMode.Choice)
        {
            MoveSingleTargetIndicator(1);
        }
    }

    public void RegisterInputHandlers()
    {
        _isRegistered = true;

        var playerInputHandler = InputManager.Instance.GetPlayerInputHandler(_attatchedPlayer);
        playerInputHandler.ConfirmButtonPress += HandleConfirmButton;
        playerInputHandler.BackButtonPress += HandleBackButton;
        playerInputHandler.LeftButtonPress += HandleLeftButton;
        playerInputHandler.RightButtonPress += HandleRightButton;
    }

    public void UnregisterInputHandlers()
    {
        _isRegistered = false;

        var playerInputHandler = InputManager.Instance.GetPlayerInputHandler(_attatchedPlayer);
        playerInputHandler.ConfirmButtonPress -= HandleConfirmButton;
        playerInputHandler.BackButtonPress -= HandleBackButton;
        playerInputHandler.LeftButtonPress -= HandleLeftButton;
        playerInputHandler.RightButtonPress -= HandleRightButton;

    }

    public void UnregisterInputHandlersIfRegistered()
    {
        if (_isRegistered)
        {
            UnregisterInputHandlers();
        }
    }
}

public class ExecuteableAction
{
    public List<Character> Targets;
    public Character Targeter;
    public TargetedAction ActionToApply;

    public ExecuteableAction(Character targeter, TargetedAction action)
    {
        Targeter = targeter;
        ActionToApply = action;
        Targets = new List<Character>();
    }
}
