using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public KeyCode KeyCode = KeyCode.P;
    public AudioSource audioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode))
        {
            Play();
        }
    }

    public void Play()
    {
        audioSource.Play();
    }


}
