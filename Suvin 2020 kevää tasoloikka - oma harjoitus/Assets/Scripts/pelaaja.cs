using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelaaja : MonoBehaviour
{
    public float speed = 10f;                //Floating point variable to store the player's movement speed.
    public float hyppyvoima = 5f;
    public float kiipeysnopeus = 15f;

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private CircleCollider2D myFeet;

    private float someScale;
    private float direction = 1;
    private float posX;

    private float painovoima;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<CircleCollider2D>();
        someScale = transform.localScale.x; // assuming this is facing right
        posX = transform.position.x;
        painovoima = rb2d.gravityScale;
    }

    void Update()
    {
      
    }


    private void FixedUpdate()
    {
        //Palauttavat 1 tai -1 riippuen siitä mihin suuntaan painetaan
        float moveHorizontal = Input.GetAxis("Horizontal");
        

        //Tekee painalluksista 2D vektorin
        Vector2 movement = new Vector2(moveHorizontal, 0);

        //Liikuttaa pelaajaa vektorin suuntaan annetulla nopeudella
        rb2d.AddForce(movement * speed);

        Debug.Log(" moveHorizontal - " + moveHorizontal);
        Debug.Log(" transform.position.x - " + transform.position.x);

        if (moveHorizontal != 0)
        {
            //Kääntäminen menosuuntaan 
            if (transform.position.x < posX)
            {
                if (direction == 1)
                {
                    transform.localScale = new Vector2(-someScale, transform.localScale.y);
                    direction = -1;
                }
            }
            else
            {
                if (direction == -1)
                {
                    transform.localScale = new Vector2(someScale, transform.localScale.y);
                    direction = 1;
                }
            }

        }

        posX = transform.position.x;
        //Debug.Log(" posX - " + posX);
        //Debug.Log(" direction - " + direction);

        Debug.Log(myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")));
        if (Input.GetButtonDown("Jump") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 hyppy = new Vector2(0, hyppyvoima);
            rb2d.AddForce(hyppy, ForceMode2D.Impulse);
        }

        Debug.Log(" osun portaisiin - " + myFeet.IsTouchingLayers(LayerMask.GetMask("Climping")));
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Climping")))
        {
            float moveVertical = Input.GetAxis("Vertical");
            rb2d.velocity = new Vector2(moveHorizontal, moveVertical * kiipeysnopeus);
            rb2d.gravityScale = 0;
        }
        else
        {
            rb2d.gravityScale = painovoima;
        }



    }
}
