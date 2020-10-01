//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
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

                        //Invoke delegate
                        if (onEnterBattle != null)
                        {
                            onEnterBattle.Invoke();

                            if(battleScene != null)
                            {
                                battleScene.SetActive(true);
                            }
                        }
                    }


                    //reset timer
                    encounterTimer = 0f;
                }

                Debug.Log("Moving..");
            }
        }
    }

    void OnEnterBatleCallback()
    {
        IsInEncounterZone = false;
    }

    void OnExitBatleCallback()
    {
        IsInEncounterZone = true;
    }
}
