using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myrb;
    public HealthBase healthBase;

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
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float playerSwipeDuration = .1f;

    

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
        
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(triggerDeath);
    }


    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;
            animator.speed = 1.5f;
        }

        else
        {
            _currentSpeed = speed;
            animator.speed = 1f;

        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myrb.velocity = new Vector2(-_currentSpeed, myrb.velocity.y);
            
            if(myrb.transform.localScale.x != -1)
            {
                myrb.transform.DOScaleX(-1, playerSwipeDuration);
            }

            animator.SetBool(boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myrb.velocity = new Vector2(_currentSpeed, myrb.velocity.y);
            if (myrb.transform.localScale.x != 1)
            {
                myrb.transform.DOScaleX(1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
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


    private void HandleScaleJump()
    {
        myrb.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myrb.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
