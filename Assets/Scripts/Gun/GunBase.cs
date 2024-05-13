using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public KeyCode shootKey = KeyCode.LeftControl;
    public float timeBetweenShoot = 0.3f;

    private Transform playerSideReference;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null) 
        {
            playerSideReference = playerObject.transform;
        }
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
               StopCoroutine( _currentCoroutine );
            }
        }
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
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }
}
