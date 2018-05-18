using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackBuffComponent", menuName = "Action Component/Attack Buff")]
public class AttackBuffComponent : ActionComponent
{
    public float BaseAttackBuffModifier;

    public override void ExecuteAction(Character target, Character targeter, float modifier)
    {
        int modifiedAttackBuffModifier = (int)(modifier * BaseAttackBuffModifier);
        target.ApplyAttackBuff(modifiedAttackBuffModifier);
    }
}