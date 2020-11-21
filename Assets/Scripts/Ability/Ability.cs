using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAbilityType
{
    NONE,
    ATTACK,
    DEFENSE,
    BUFF
}

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/Ability", order = 1)]
public abstract class Ability : ScriptableObject
{
    public string abilityName = "";
    public AnimationClip animationClip = null;
    public AnimationClip enemyAnimationClip = null;
    public EAbilityType abilityType = EAbilityType.NONE;

    public abstract void ApplyEffects(ICharacter caster, ICharacter target);
}
