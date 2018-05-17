using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealComponent", menuName = "Action Component/Heal")]
public class HealComponent : ActionComponent
{
    public int BaseHealing;

    public override void ExecuteAction(Character target, Character targeter, float modifier)
    {
        int modifiedHealing = (int)(targeter.HealModifier * modifier * BaseHealing);
        target.ApplyHealing(modifiedHealing);
    }
}