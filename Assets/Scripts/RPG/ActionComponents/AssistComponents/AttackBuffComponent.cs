using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackBuffComponent", menuName = "Action Component/Attack Buff")]
public class AttackBuffComponent : ActionComponent
{
    public float BaseAttackBuffModifier;

    public override void ExecuteAction(Character target, Character targeter, float modifier)
    {
        if (targeter is PlayerCharacter)
        {
            PeformanceModifier =  0.5f + PerformanceManager.Instance.GetCharacterPerformance((PlayerCharacter)targeter);
        }
        else
        {
            PeformanceModifier = 1.0f;
        }

        int modifiedAttackBuffModifier = (int)(modifier * BaseAttackBuffModifier * PeformanceModifier);
        target.ApplyAttackBuff(modifiedAttackBuffModifier);
    }
}