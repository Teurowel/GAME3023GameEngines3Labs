using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDance", menuName = "ScriptableObjects/Abilities/AbilityDance", order = 3)]
public class AbilityDance : Ability
{
    public override void ApplyEffects(ICharacter caster, ICharacter target)
    {
        Debug.Log("Ability Dance!");
        target.GetDamage(caster.Damage);
    }
}
