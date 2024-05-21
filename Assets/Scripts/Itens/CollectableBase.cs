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
    public Collider2D collider2D;

    [Header("Sounds")]
    public AudioSource audioSource;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager instance is null.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        if (collider2D != null) collider2D.enabled = false;
        Invoke("HideObject", timeToHide);
        OnCollect();
        gameManager.DecreaseCollectableCount();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}

