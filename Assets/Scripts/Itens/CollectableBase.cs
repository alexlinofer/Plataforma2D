using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 3f;
    public GameObject graphicItem;
    //public ParticleSystem particleSystemPrefab;

    [Header("Sounds")]
    public AudioSource audioSource;
    //public AudioSource audioSourcePrefab;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
       if(graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollect();

        //gameObject.SetActive(false);     
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null) audioSource.Play();


        /*
        if (audioSourcePrefab != null)
        {

            AudioSource instantiatedAudioSource = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            instantiatedAudioSource.Play();

            Destroy(instantiatedAudioSource.gameObject, instantiatedAudioSource.clip.length);
        }


            if (particleSystemPrefab != null)
        {
            ParticleSystem instantiatedParticleSystem = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            instantiatedParticleSystem.Play();

            Destroy(instantiatedParticleSystem.gameObject, instantiatedParticleSystem.main.duration);
        }*/
    }
    

}

