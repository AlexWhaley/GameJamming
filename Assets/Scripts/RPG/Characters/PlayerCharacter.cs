using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    [Header("Character Actions")]
    [SerializeField]
    private List<Action> _attackActions;
    [SerializeField]
    private List<Action> _assistActions;
    [SerializeField]
    private List<Action> _healActions;

    public List<Action> AttackActions { get { return _attackActions; } }
    public List<Action> AssistActions { get { return _assistActions; } }
    public List<Action> HealActions { get { return _healActions; } }
}
