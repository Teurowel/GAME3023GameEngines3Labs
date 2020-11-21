using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public int passiveOrAggressive = 0; // 0 == passive, aggrssive = 100

    public Animator Animator { get; set; }
    public AnimatorOverrideController AnimatorOverride { get; set; }

    public AnimationClip replaceableAbilityAnim = null;
    public UnityEvent onAnimFinished; //Battlemanager subscirbe this


    List<int> attackAbilityList = new List<int>();
    List<int> defenseAbilityList = new List<int>();
    List<int> buffAbilityList = new List<int>();


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

    public void DecisionMaking(ICharacter player)
    {
        int abilityIdx = 0;
        int listIdx = 0;

        //Decide what ability to use based on passive or aggressive
        int randomNum = Random.Range(0, 101); //value from 0 ~ 100
        if (randomNum <= passiveOrAggressive) //80
        {
            //Aggressvie pattern

            //Randomly select attack ability
            Debug.Log("Enemy attack");
            listIdx = Random.Range(0, attackAbilityList.Count);
            abilityIdx = attackAbilityList[listIdx];
        }
        else
        {
            //Passive pattern

            //If enemy has more than half health, do buff
            if (((float)hp / (float)hpMax) >= 0.5)
            {
                Debug.Log("Enemy buff");
                //Randomly select buff ability
                listIdx = Random.Range(0, buffAbilityList.Count);
                abilityIdx = buffAbilityList[listIdx];
            }
            //otherwise, do defense
            else
            {
                Debug.Log("Enemy defnese");
                //Randomly select defense ability
                listIdx = Random.Range(0, defenseAbilityList.Count);
                abilityIdx = defenseAbilityList[listIdx];
            }
        }

        //Apply effect
        Abilities[abilityIdx].ApplyEffects(this, player);

        AnimatorOverride[replaceableAbilityAnim.name] = Abilities[abilityIdx].enemyAnimationClip;
        Animator.SetTrigger("AbilityAnimationTrigger");
    }

    public void AnimFinished()
    {
        Debug.Log("Enemy Animation Finished");
        if (onAnimFinished != null)
        {
            onAnimFinished.Invoke();
        }
    }

    public void SetAbilityListBasedOnType()
    {
        attackAbilityList.Clear();
        defenseAbilityList.Clear();
        buffAbilityList.Clear();

        for(int i = 0; i < abilities.Length; ++i)
        {
            if(abilities[i].abilityType == EAbilityType.ATTACK)
            {
                attackAbilityList.Add(i);
            }
            else if (abilities[i].abilityType == EAbilityType.DEFENSE)
            {
                defenseAbilityList.Add(i);
            }
            else if (abilities[i].abilityType == EAbilityType.BUFF)
            {
                buffAbilityList.Add(i);
            }
        }
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
