using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DamageComponent", menuName = "Action Component/Damage")]
public class DamageComponent : ActionComponent
{
    public int BaseDamage;

    public override void ExecuteAction(Character target, Character targeter, float modifier)
    {
        int modifiedDamage = (int)(targeter.AttackModifier * BaseDamage * modifier);
        target.ApplyDamage(modifiedDamage);
    }
}