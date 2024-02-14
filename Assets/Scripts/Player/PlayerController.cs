using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myrb;

    public Vector2 friction = new Vector2(1f, 0);

    public float speed;
    public float speedRun;

    private float _currentSpeed;

    public float forceJump = 10;

    private bool _isJumping = false;

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;
       

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myrb.MovePosition(myrb.position - velocity * Time.deltaTime);
            myrb.velocity = new Vector2(-_currentSpeed, myrb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myrb.MovePosition(myrb.position + velocity * Time.deltaTime);
            myrb.velocity = new Vector2(_currentSpeed, myrb.velocity.y);

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
        if (Input.GetKeyDown(KeyCode.UpArrow) && !_isJumping)
        {
            StartCoroutine(JumpTimer());
        }

    }

    IEnumerator JumpTimer()
    {
         _isJumping = true;
         myrb.velocity = Vector2.up * forceJump;
         yield return new WaitForSeconds(1);
         _isJumping = false;
    }
}
