using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour, ICharacter
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
    public AnimationClip replaceableAbilityAnim = null;

    public UnityEvent onAnimFinished; //Battlemanager subscirbe this

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();

        AnimatorOverride = new AnimatorOverrideController(Animator.runtimeAnimatorController); //create new Animator Override Controller
        Animator.runtimeAnimatorController = AnimatorOverride; //set animator override controller as current run time controller
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceAbilityAnimationClip(int abilityIdx)
    {
        AnimatorOverride[replaceableAbilityAnim.name] = Abilities[abilityIdx].animationClip;
    }

    public void AnimFinished()
    {
        Debug.Log("Animation Finished");
        if(onAnimFinished != null)
        {
            onAnimFinished.Invoke();
        }
    }

    #region ICharacter
    public void OnTurnBegin()
    {
        Debug.Log("Player turn begin");
    }

    public void OnTurnEnd()
    {
        Debug.Log("Player turn end");
    }

    public void GetDamage(int damage)
    {
        Debug.Log("Player got damage");

        hp -= damage;
    }
    #endregion
}
