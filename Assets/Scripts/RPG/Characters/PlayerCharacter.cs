using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public enum CharacterNames
    {
        Pick,
        Nome,
        Yama,
        Four
    }

    [Space(10), SerializeField]
    private CharacterNames _characterName;
    public CharacterNames CharacterName { get { return _characterName; } }

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
