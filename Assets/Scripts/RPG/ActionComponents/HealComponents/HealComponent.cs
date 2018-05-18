using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealComponent", menuName = "Action Component/Heal")]
public class HealComponent : ActionComponent
{
    public int BaseHealing;

    public override void ExecuteAction(Character target, Character targeter, float modifier)
    {
        if (targeter is PlayerCharacter)
        {
            PeformanceModifier = 0.5f + PerformanceManager.Instance.GetCharacterPerformance((PlayerCharacter)targeter);
        }
        else
        {
            PeformanceModifier = 1.0f;
        }

        int modifiedHealing = (int)(targeter.HealModifier * modifier * BaseHealing);
        target.ApplyHealing(modifiedHealing);
    }
}