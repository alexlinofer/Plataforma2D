using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystemPrefab;
    //public float timeToHide = 0.5f;
    //public GameObject graphicItem;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        //if(graphicItem != null) graphicItem.SetActive(false);
        OnCollect();
        gameObject.SetActive(false);
        //Invoke(nameof(HideObject), timeToHide);
    }

    /*private void HideObject()
    {
        gameObject.SetActive(false);
    }*/

    protected virtual void OnCollect()
    {
        //particleSystemPrefab.Play();

        if(particleSystemPrefab != null)
        {
            ParticleSystem instantiatedParticleSystem = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            instantiatedParticleSystem.Play();

            Destroy(instantiatedParticleSystem.gameObject, instantiatedParticleSystem.main.duration);
        }
    }
    

}

