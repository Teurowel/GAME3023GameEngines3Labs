using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float speed = 10f;

    

    [SerializeField]
    GameObject battleScene; //Battle scene when we encounter enemy

    // Start is called before the first frame update
    void Start()
    {
        //Subscribe to event
        BattleManager.instance.onEnterBattle += OnEnterBattleCallBack;
        BattleManager.instance.onExitBattle += OnExitBattleCallBack;
    }

    //// Update is called once per frame
    void Update()
    {
        //Set velocity of player
        float horizontalAxis = Input.GetAxis("Horizontal"); // a, d
        float verticalAxis = Input.GetAxis("Vertical");//w, s

        rb.velocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        BattleManager.instance.IsInEncounterZone = true;

        //InvokeRepeating(StartRandomEncounter, )
        //StartCoroutine(startR)
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        BattleManager.instance.IsInEncounterZone = false;

        //Reset timer
       // encounterTimer = 0f;
    }

    void OnEnterBattleCallBack()
    {
        enabled = false;

        //Stop moving player
        rb.velocity = Vector3.zero;
        //gameObject.SetActive(false);
    }

    void OnExitBattleCallBack()
    {
        enabled = true;
        //gameObject.SetActive(false);
    }

}
