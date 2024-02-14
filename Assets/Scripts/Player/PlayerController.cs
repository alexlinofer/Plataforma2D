using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myrb;


    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 10;

    private bool _isJumping = false;
    private float _currentSpeed;


    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    /*public float squishyScaleX = 1.5f;
    public float squishyScaleY = 0.7f;
    public float squishyanimationDuration = 0.3f;*/
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;
    //public GameObject player;
    


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
            HandleScaleJump();
        }
    }

    IEnumerator JumpTimer()
    {
        _isJumping = true;
        myrb.velocity = Vector2.up * forceJump;
        myrb.transform.localScale = Vector2.one;
        DOTween.Kill(myrb.transform);


         yield return new WaitForSeconds(0.9f);
         _isJumping = false;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
      if (gameObject.CompareTag("Floor") && !_isJumping)
        {
            myrb.transform.DOScaleX(squishyScaleX, squishyanimationDuration).SetLoops(2, LoopType.Yoyo);
            myrb.transform.DOScaleY(squishyScaleY, squishyanimationDuration).SetLoops(2, LoopType.Yoyo);
        } 
    }*/

    private void HandleScaleJump()
    {
        myrb.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myrb.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    /*IEnumerator AnimationScaleJump()
    {
        myrb.transform.DOScaleX(squishyScaleX, squishyanimationDuration).SetLoops(2, LoopType.Yoyo);
        myrb.transform.DOScaleY(squishyScaleY, squishyanimationDuration).SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(squishyanimationDuration);
        myrb.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo);
        myrb.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo);
    }*/
}
