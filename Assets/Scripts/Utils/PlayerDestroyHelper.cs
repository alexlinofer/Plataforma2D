using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyHelper : MonoBehaviour
{
    public PlayerController playerController;

    public void KillPlayer()
    {
        playerController.DestroyMe();
    }
}
