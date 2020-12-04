//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

enum Phase
{
    PLAYER_PHASE,
    ENEMY_PHASE
}

public class BattleManager : MonoBehaviour
{
    public bool IsInEncounterZone;

    [SerializeField]
    float encounterIntervalTime = 1f; //every 1 second we check encounter
    float encounterTimer = 0f; //will be accumulated for interval check
    [SerializeField]
    float encounterChance = 25f; //Encounter chance

    public Rigidbody2D playerRB; //Player rigid body

    public GameObject battleScene;

    [SerializeField] PlayerCharacter playerCharacter = null; //player character
    ICharacter playerCharacterInterface = null;

    [SerializeField] EnemyCharacter enemyCharacter = null; //enemy character
    ICharacter enemyCharacterInterface = null;

    [SerializeField] EnemyCharacterAttributes[] enemyCharacters = null; //list of enemy characters

    [SerializeField] TMPro.TextMeshProUGUI[] abilityTexts = null;

    Phase phase = Phase.PLAYER_PHASE;

    //Delegates
    public delegate void OnEnterBattle();
    public OnEnterBattle onEnterBattle; //Anyone who wants to know if player enters battle, subscribe to this

    public delegate void OnExitBattle();
    public OnExitBattle onExitBattle; //Anyone who wants to know if player exit battle, subscribe to this

    #region Singleton
    public static BattleManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        onEnterBattle += OnEnterBatleCallback;
        onExitBattle += OnExitBatleCallback;

        if(playerCharacter != null)
        {
            playerCharacterInterface = playerCharacter.GetComponent<ICharacter>();
            playerCharacter.onAnimFinished.AddListener(OnPlayerAnimFinishedCallback); //Add call back funtion to player anim fisnihed
        }

        if (enemyCharacter != null)
        {
            enemyCharacterInterface = enemyCharacter.GetComponent<ICharacter>();
            enemyCharacter.onAnimFinished.AddListener(OnEnemyAnimFinishedCallback); //Add call back funtion to player anim fisnihed
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckEncounterZone();

        if(phase == Phase.ENEMY_PHASE)
        {


            //Debug.Log("enemy animation finished, turn chagned to player");
            //phase = Phase.PLAYER_PHASE;
        }
    }

    void CheckEncounterZone()
    {
        //If we are in Encounter Zone...
        if (IsInEncounterZone == true)
        {
            //If we are moving....
            if (playerRB.velocity.magnitude != 0)
            {
                //Start counting
                encounterTimer += Time.deltaTime;

                //if reach interval time...
                if (encounterTimer >= encounterIntervalTime)
                {
                    Debug.Log("Check chance");

                    //Check encounter chance
                    if (Random.Range(1f, 100f) <= encounterChance)
                    {
                        Debug.Log("Encounter!!!");

                        if (onEnterBattle != null)
                        {
                            onEnterBattle.Invoke();
                        }

                        //Fade screen to black first
                        ScreenOverlayManager.Instance.FadeToBlack();

                        //After fade to black, do sth
                        Invoke("ToDoOnEnterBattle", 2.0f);
                    }


                    //reset timer
                    encounterTimer = 0f;
                }

                Debug.Log("Moving..");
            }
        }
    }

    void ToDoOnEnterBattle()
    {
        //Show battle screen
        if (battleScene != null)
        {
            battleScene.SetActive(true);

            SetRandomEnemy();

            SetAbilityButtonText();
        }

        //Fade screen from black
        ScreenOverlayManager.Instance.FadeFromBlack();
    }

    void SetAbilityButtonText()
    {
        for (int i = 0; i < playerCharacterInterface.Abilities.Length; ++i)
        {
            if (playerCharacterInterface.Abilities[i] != null)
            {
                abilityTexts[i].text = playerCharacterInterface.Abilities[i].abilityName;
            }
            else
            {
                abilityTexts[i].text = "none";
            }
        }
    }

    void SetRandomEnemy()
    {
        //Set random enemy from array
        enemyCharacter.Hp = enemyCharacters[0].Hp;
        enemyCharacter.HpMax = enemyCharacters[0].HpMax;
        enemyCharacter.Damage = enemyCharacters[0].Damage;
        enemyCharacter.Abilities = enemyCharacters[0].Abilities;
        enemyCharacter.passiveOrAggressive = enemyCharacters[0].passiveOrAggressive;

        //Set ability list absed on ability type
        enemyCharacter.SetAbilityListBasedOnType();
    }

    void OnEnterBatleCallback()
    {
        IsInEncounterZone = false;
    }

    void OnExitBatleCallback()
    {
        IsInEncounterZone = true;
    }

    public void OnAbilityButtonClicked(int buttonIdx)
    {
        //Only works on player phase
        if (phase == Phase.PLAYER_PHASE)
        {
            if (playerCharacterInterface.Abilities[buttonIdx] != null && enemyCharacterInterface != null)
            {
                playerCharacterInterface.Abilities[buttonIdx].ApplyEffects(playerCharacterInterface, enemyCharacterInterface);

                playerCharacter.ReplaceAbilityAnimationClip(buttonIdx);

                playerCharacterInterface.Animator.SetTrigger("AbilityAnimationTrigger");
            }
        }
    }

    public void OnPlayerAnimFinishedCallback()
    {
        Debug.Log("Player animation finished, turn chagned to enemy");
        
        phase = Phase.ENEMY_PHASE;
        enemyCharacter.DecisionMaking(playerCharacter);
    }

    public void OnEnemyAnimFinishedCallback()
    {
        Debug.Log("enemy animation finished, turn chagned to player");
        phase = Phase.PLAYER_PHASE;
    }
}
