using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _attatchedPlayer;
    public PlayerCharacter AttatchedPlayer { get { return _attatchedPlayer; }}

    [SerializeField] private ActionMenuController _actionMenuController;

    [SerializeField] private PlayerActionTargeter _playerActionTargeter;

    public void OpenActionMenu(bool isFreshOpen)
    {
        _actionMenuController.RevealActionMenu(isFreshOpen);
    }

    public void InitiateTargettingSequence(Action selectedAction)
    {
        _playerActionTargeter.InitiateTargettingSequence(selectedAction);
    }

    public void LockInActionTargets()
    {
        _actionMenuController.DisplayReadyMessage();
    }
}
