using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public KeyCode shootKey = KeyCode.LeftControl;
    public float timeBetweenShoot = 0.3f;
    public AudioRandomPlayAudioClips audioRandomPlayAudioClips;

    private Transform playerSideReference;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        UpdatePlayerReference();
    }

    private void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(shootKey))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
        UpdatePlayerReference();
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        if (audioRandomPlayAudioClips != null)
        {
            audioRandomPlayAudioClips.PlayRandom();
        }

        if (prefabProjectile != null && positionToShoot != null)
        {
            var projectile = Instantiate(prefabProjectile);
            projectile.transform.position = positionToShoot.position;

            if (playerSideReference != null)
            {
                projectile.side = playerSideReference.localScale.x;
            }
            else
            {
                Debug.LogError("Player side reference is not assigned or has been destroyed.");
                UpdatePlayerReference(); // Tentar atualizar a referência do jogador novamente
            }
        }
        else
        {
            Debug.LogError("Projectile or Position to Shoot is not assigned.");
        }
    }

    public void UpdatePlayerReference()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerSideReference = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }
}
