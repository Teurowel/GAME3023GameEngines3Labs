using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    Animator animator;

    [SerializeField]
    float speed = 10f;

    

    [SerializeField]
    GameObject battleScene; //Battle scene when we encounter enemy


    private void Awake()
    {
        //Subscirbe to saver event
        Saver.OnSave.AddListener(OnSaveCallBack);
        Saver.OnLoad.AddListener(OnLoadCallBack);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Subscribe to event
        BattleManager.instance.onEnterBattle += OnEnterBattleCallBack;
        BattleManager.instance.onExitBattle += OnExitBattleCallBack;

        animator = GetComponent<Animator>();

    }

    //// Update is called once per frame
    void Update()
    {
        //Set velocity of player
        float horizontalAxis = Input.GetAxis("Horizontal"); // a, d
        float verticalAxis = Input.GetAxis("Vertical");//w, s

        rb.velocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);

        //Set animator paramter for walk animation
        animator.SetFloat("Horizontal", horizontalAxis);
        animator.SetFloat("Vertical", verticalAxis);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);



        //Set animator paramter for idle animation
        if(horizontalAxis >= 0.1 || horizontalAxis <= -0.1 || verticalAxis >= 0.1 || verticalAxis <= -0.1)
        {
            animator.SetFloat("LastHorizontal", horizontalAxis);
            animator.SetFloat("LastVertical", verticalAxis);
        }
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

    void OnSaveCallBack()
    {
        //Save player's position as string
        string dataToStore = "";

        dataToStore = transform.position.x + "," + transform.position.y + "," + transform.position.z;

        PlayerPrefs.SetString("PlayerPosition", dataToStore);
        Debug.Log("Player position saved");
    }

    void OnLoadCallBack()
    {
        //Load player's position as string
        if(PlayerPrefs.HasKey("PlayerPosition"))
        {
            string saveData = PlayerPrefs.GetString("PlayerPosition", "");

            char[] delimiters = new char[] { ',' };

            //Splite string data
            string[] spliteData = saveData.Split(delimiters);

            //parse to get float
            float x = float.Parse(spliteData[0]);
            float y = float.Parse(spliteData[1]);
            float z = float.Parse(spliteData[2]);

            transform.position = new Vector3(x, y, z);
            Debug.Log("Player position loaded");

        }
    }
}
