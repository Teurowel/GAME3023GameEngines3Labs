using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityBuff", menuName = "ScriptableObjects/Abilities/AbilityBuff", order = 4)]
public class AbilityBuff : Ability
{
    public override void ApplyEffects(ICharacter caster, ICharacter target)
    {
        Debug.Log("Ability Buff!");
        target.GetDamage(caster.Damage);
    }
}
