using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCharacter", menuName = "ScriptableObjects/EnemyCharacter/Enemy", order = 1)]
public class EnemyCharacterAttributes : ScriptableObject
{
    public int Hp = 0;
    public int HpMax = 0;
    public int Damage = 0;
    public Ability[] Abilities = null;

    [Range(0, 100)]
    public int passiveOrAggressive = 0; // 0 == passive, aggrssive = 100
}
