using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCharacter : MonoBehaviour, ICharacter
{
    [SerializeField] private int hp = 100;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    [SerializeField] private int hpMax = 100;
    public int HpMax
    {
        get
        {
            return hpMax;
        }
        set
        {
            hpMax = value;
        }
    }

    [SerializeField] private int damage = 10;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    [SerializeField] private Ability[] abilities;
    public Ability[] Abilities
    {
        get
        {
            return abilities;
        }
        set
        {
            abilities = value;
        }
    }

    public Animator Animator { get; set; }
    public AnimatorOverrideController AnimatorOverride { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ICharacter
    public void OnTurnBegin()
    {
        Debug.Log("Enemy turn begin");
    }

    public void OnTurnEnd()
    {
        Debug.Log("Enemy turn end");
    }

    public void GetDamage(int damage)
    {
        Debug.Log("Enemy got damage");

        hp -= damage;
    }
    #endregion
}
