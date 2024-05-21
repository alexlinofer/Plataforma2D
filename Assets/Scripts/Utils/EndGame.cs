using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public AudioSource victorySound;
    public PauseManager pauseManager;
    public GameObject regularUI;
    public TextMeshProUGUI textMeshProUGUI;
    public GameManager gameManager;

    private void OnEnable()
    {
        PlayVictorySound();
        pauseManager.Pause();
        regularUI.SetActive(false);
        textMeshProUGUI.text = gameManager.timer.ToString("F2");

    }

    private void PlayVictorySound()
    {
        if (victorySound != null) victorySound.Play();
    }

    public void ShowEndGameScreen()
    {
        gameObject.SetActive(true);
    }

}
