using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelaaja_opetus : MonoBehaviour
{

    public float speed = 10f;
    public float hyppyvoima = 5f;
    public float kiipeilynopeus = 5f;

    private Rigidbody2D rb2d;

    private float direction;
    private float posX;

    private CircleCollider2D myFeet;
    private float painovoima;
    private Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        direction = 1;
        posX = transform.position.x;
        myFeet = GetComponent<CircleCollider2D>();
        painovoima = rb2d.gravityScale;
        anime = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");


        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

        if (moveHorizontal != 0)
        {
            anime.SetBool("walking", true);

            if (transform.position.x < posX)
            {
                /*Debug.Log("Moving left - " + transform.position.x);*/
                if (direction == 1)
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                    direction = -1;
                }
            }
            else
            {
                /*Debug.Log("Moving right - " + transform.position.x);*/
                if (direction == -1)
                {
                    transform.localScale = new Vector2(1, transform.localScale.y);
                    direction = 1;
                }
            }

            posX = transform.position.x;
        }
        else
        {
            anime.SetBool("walking", false);
        }

        Debug.Log("jalat osuvat maahan" + myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")));
        if (Input.GetButtonDown("Jump") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 hyppy = new Vector2(0, hyppyvoima);
            rb2d.AddForce(hyppy, ForceMode2D.Impulse);
        }

        Debug.Log("jalat osuvat portaisiin" + myFeet.IsTouchingLayers(LayerMask.GetMask("Climping")));

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Climping")))
        {
            float moveVertical = Input.GetAxis("Vertical");
            rb2d.velocity = new Vector2(moveHorizontal, moveVertical * kiipeilynopeus);
            rb2d.gravityScale = 0;
        }
        else
        {
            rb2d.gravityScale = painovoima;
        }
    }
}
