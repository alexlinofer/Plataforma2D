using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTriggerTransition : MonoBehaviour
{
    public AudioMixerSnapshot snapshot01;
    public AudioMixerSnapshot snapshot02;

    public float timeToTransition = 0.2f;

    public string tagToCompare = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagToCompare))
        {
            snapshot02.TransitionTo(timeToTransition);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagToCompare))
        {
            snapshot01.TransitionTo(timeToTransition);
        }
    }

}
