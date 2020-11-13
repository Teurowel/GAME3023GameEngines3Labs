using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/Ability", order = 1)]
public abstract class Ability : ScriptableObject
{
    public string abilityName = "";
    public AnimationClip animationClip = null;

    public abstract void ApplyEffects(ICharacter caster, ICharacter target);
}
