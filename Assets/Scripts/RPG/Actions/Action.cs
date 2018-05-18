using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Action")]
public class Action : ScriptableObject
{
    public string ActionName;
    public string ActionDescription;
    public List<TargetedAction> TargetedActions;
}

[Serializable]
public class TargetedAction
{
    public string ActionPartName;
    public string SelectionPromptMessage;
    public TargetType TargetType;
    public List<ActionComponent> ActionComponents;
}

public enum TargetType
{
    EnemySingle,
    PlayersSingle,
    AllEnemiesInclusive,
    AllEnemiesExclusive,
    AllPlayersInclusive,
    AllPlayersExclusive,
    Self
}

[Serializable]
public class EnemyTargetedAction : TargetedAction
{
    public string TargetPreferenceName;
    public bool TargetLowest = false;
}
