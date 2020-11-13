//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    int Hp { get; set; }
    int HpMax { get; set; }

    int Damage { get; set; }

    Ability[] Abilities { get; set; }

    Animator Animator { get; set; }
    AnimatorOverrideController AnimatorOverride { get; set; } //to swap animation clip

    void OnTurnBegin();
    void OnTurnEnd();

    void GetDamage(int damage);
}
