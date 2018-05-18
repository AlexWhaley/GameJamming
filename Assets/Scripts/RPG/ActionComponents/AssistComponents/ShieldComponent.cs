using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShieldComponent", menuName = "Action Component/Shield")]
public class ShieldComponent : ActionComponent
{
    public float BaseShieldModifier;

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

        int modifiedShieldModifier = (int)(modifier * BaseShieldModifier);
        target.ApplyShield(modifiedShieldModifier);
    }
}