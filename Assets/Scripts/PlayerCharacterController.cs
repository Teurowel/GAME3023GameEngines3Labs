using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float speed = 10f;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    void Update()
    {
        //Set velocity of player
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);
    }

}
