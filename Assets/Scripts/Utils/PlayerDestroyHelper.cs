using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyHelper : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            playerController.OnPlayerDestroyed += KillPlayer;
        }
    }

    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.OnPlayerDestroyed -= KillPlayer;
        }
    }

    public void KillPlayer()
    {
        playerController.DestroyMe();
    }
}
