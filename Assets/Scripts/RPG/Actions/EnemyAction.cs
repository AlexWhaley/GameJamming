using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Action", menuName = "Enemy Action")]
public class EnemyAction : ScriptableObject
{
    public string ActionName;
    public string ActionDescription;
    public List<EnemyTargetedAction> TargetedActions;
}