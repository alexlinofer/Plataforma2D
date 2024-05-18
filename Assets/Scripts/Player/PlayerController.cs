using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myrb;
    public HealthBase healthBase;

    public event System.Action OnPlayerDestroyed;

    [Header("Player Setup")]
    public SOPlayerSetup soPlayerSetup;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = 0.1f;
    public ParticleSystem jumpVFX;


    //public Animator animator;

    //private bool _isJumping = false;
    private float _currentSpeed;
    private Animator _currentPlayer;


    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if(collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
        
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }


    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 1.5f;
        }

        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1f;

        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myrb.velocity = new Vector2(-_currentSpeed, myrb.velocity.y);
            
            if(myrb.transform.localScale.x != -1)
            {
                myrb.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }

            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myrb.velocity = new Vector2(_currentSpeed, myrb.velocity.y);
            if (myrb.transform.localScale.x != 1)
            {
                myrb.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        if(myrb.velocity.x > 0)
        {
            myrb.velocity -= soPlayerSetup.friction;
        }
        else if(myrb.velocity.x < 0)
        {
            myrb.velocity += soPlayerSetup.friction;
        }

    }


    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            myrb.velocity = Vector2.up * soPlayerSetup.forceJump;
            myrb.transform.localScale = Vector2.one;

            DOTween.Kill(myrb.transform);


            //StartCoroutine(JumpTimer());
            PlayJumpVFX();
            HandleScaleJump();
        }
    }

    private void PlayJumpVFX()
    {
        if(jumpVFX != null)
        {
            jumpVFX.Play();
        }
    }

    /*IEnumerator JumpTimer()
    {
        _isJumping = true;
        myrb.velocity = Vector2.up * soPlayerSetup.forceJump;
        myrb.transform.localScale = Vector2.one;
        DOTween.Kill(myrb.transform);


         yield return new WaitForSeconds(0.9f);
         _isJumping = false;
    }*/


    private void HandleScaleJump()
    {
        myrb.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myrb.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        OnPlayerDestroyed?.Invoke();
        Destroy(gameObject);
    }

}
