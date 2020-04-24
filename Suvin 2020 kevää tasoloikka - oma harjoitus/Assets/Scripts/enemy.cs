using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 1f;
    public bool eteenpain = true;
    private Rigidbody2D rb2d;
    private float suunta = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (eteenpain == false)
        {
            suunta = -1;
        }
        transform.localScale = new Vector2(suunta, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (eteenpain == false)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        
    }
}
