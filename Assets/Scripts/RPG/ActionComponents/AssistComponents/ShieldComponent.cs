using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShieldComponent", menuName = "Action Component/Shield")]
public class ShieldComponent : ActionComponent
{
    public float BaseShieldModifier;
    public int BaseShieldDuration;

    public override void ExecuteAction(Character target, float modifier)
    {
        int modifiedShieldModifier = (int)(modifier * BaseShieldModifier);
        target.ApplyShield(modifiedShieldModifier, BaseShieldDuration);
    }
}