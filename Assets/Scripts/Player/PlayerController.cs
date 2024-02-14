using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myrb;

    public Vector2 friction = new Vector2(1f, 0);

    public float speed;

    public float forceJump = 10;

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myrb.MovePosition(myrb.position - velocity * Time.deltaTime);
            myrb.velocity = new Vector2(-speed, myrb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myrb.MovePosition(myrb.position + velocity * Time.deltaTime);
            myrb.velocity = new Vector2(speed, myrb.velocity.y);

        }

        if(myrb.velocity.x > 0)
        {
            myrb.velocity -= friction;
        }
        else if(myrb.velocity.x < 0)
        {
            myrb.velocity += friction;
        }

    }


    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myrb.velocity = Vector2.up * forceJump;

        }
    }
}
