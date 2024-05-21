using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myrb;
    public HealthBase healthBase;

    public event System.Action OnPlayerDestroyed;

    [Header("Player Setup")]
    public SOPlayerSetup soPlayerSetup;
    public AudioSource audioJumper;
    public Transform spawnPoint;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = 0.1f;
    public ParticleSystem jumpVFX;

    private float _currentSpeed;
    private Animator _currentPlayer;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        // Procura o spawnPoint na cena pela tag
        GameObject spawnPointObject = GameObject.FindWithTag("SpawnPoint");
        if (spawnPointObject != null)
        {
            spawnPoint = spawnPointObject.transform;
        }
        else
        {
            Debug.LogError("Spawn point not found in the scene.");
        }

        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }

        // Atualize a referência do jogador no GunBase
        UpdateGunBasePlayerReference();
    }

    public void Respawn()
    {
        _currentPlayer = Instantiate(soPlayerSetup.player, spawnPoint.transform);

        // Atualize a referência do jogador no GunBase
        UpdateGunBasePlayerReference();
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

        // Chame o método de respawn após uma pequena espera para simular a morte
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(2); // ajuste o tempo conforme necessário
        Respawn();
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

            if (myrb.transform.localScale.x != -1)
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

        if (myrb.velocity.x > 0)
        {
            myrb.velocity -= soPlayerSetup.friction;
        }
        else if (myrb.velocity.x < 0)
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
            audioJumper.Play();

            DOTween.Kill(myrb.transform);

            PlayJumpVFX();
            HandleScaleJump();
        }
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
    }

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

    private void UpdateGunBasePlayerReference()
    {
        GunBase gunBase = FindObjectOfType<GunBase>();
        if (gunBase != null)
        {
            gunBase.UpdatePlayerReference();
        }
    }
}
