using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myrb;

    public Vector2 velocity;

    public float speed;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //myrb.MovePosition(myrb.position - velocity * Time.deltaTime);
            myrb.velocity = new Vector2(-speed, velocity.y);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myrb.MovePosition(myrb.position + velocity * Time.deltaTime);
            myrb.velocity = new Vector2(speed, velocity.y);

        }
    }


}
