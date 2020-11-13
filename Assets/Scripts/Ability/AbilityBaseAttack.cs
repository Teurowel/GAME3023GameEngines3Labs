using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityBaseAttack", menuName = "ScriptableObjects/Abilities/AbilityBaseAttack", order = 2)]
public class AbilityBaseAttack : Ability
{
    public override void ApplyEffects(ICharacter caster, ICharacter target)
    {
        Debug.Log("Ability Base Attack!");
        target.GetDamage(caster.Damage);
    }
}
