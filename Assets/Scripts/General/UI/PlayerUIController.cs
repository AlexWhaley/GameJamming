using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _attatchedPlayer;
    [SerializeField] private Color _hudColor;

    public PlayerCharacter AttatchedPlayer { get { return _attatchedPlayer; }}

    [SerializeField] private ActionMenuController _actionMenuController;

    [SerializeField] private PlayerActionTargeter _playerActionTargeter;

    public void OpenActionMenu(bool isFreshOpen)
    {
        _actionMenuController.RevealActionMenu(isFreshOpen);
    }

    public void EndActionSelection()
    {
        _actionMenuController.HideActionMenu();
        _playerActionTargeter.RemoveAllIndicators();
    }

    public void InitiateTargettingSequence(Action selectedAction)
    {
        _playerActionTargeter.InitiateTargettingSequence(selectedAction);
    }

    public void LockInActionTargets()
    {
        _actionMenuController.DisplayReadyMessage();
    }

    public void UnlockInActionTargets()
    {
        _actionMenuController.HideReadyMessage();
    }

    public Color HudColor
    {
        get { return _hudColor; }
    }
}
