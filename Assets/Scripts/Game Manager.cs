using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plataforma2D.Singleton;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GameManager : Plataforma2D.Singleton.Singleton<GameManager>
{
    public GameObject endGameScreen;
    public TextMeshProUGUI textMeshProUGUI1;
    public string tagToCheckEndGame = "Collectable";

    public GameObject playerPrefab;
    public Transform startPoint;

    public float timer;
    private bool _isTimerRunning;
    private int _collectableCount;
    private GameObject _currentPlayer;

    protected override void Awake()
    {
        base.Awake(); // Certifique-se de chamar a implementação base para configurar a instância Singleton.
        Debug.Log("TimeScale no Awake: " + Time.timeScale);
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        StartTimer();
        _collectableCount = GameObject.FindGameObjectsWithTag(tagToCheckEndGame).Length;
        Debug.Log("Initial collectable count: " + _collectableCount);
        RespawnPlayer();
    }

    public void StartTimer()
    {
        timer = 0f;
        _isTimerRunning = true;
    }

    public void StopTimer()
    {
        _isTimerRunning = false;
    }

    private void Update()
    {
        if (_isTimerRunning)
        {
            timer += Time.deltaTime;
            if (textMeshProUGUI1 != null)
            {
                textMeshProUGUI1.text = timer.ToString("F2");
            }
            else
            {
                Debug.LogError("textMeshProUGUI1 is not assigned.");
            }
        }
    }

    public void DecreaseCollectableCount()
    {
        _collectableCount--;
        Debug.Log("Collectable count decreased: " + _collectableCount);
        if (_collectableCount == 0)
        {
            Debug.Log("All collectables collected, calling end game.");
            CallEndgame();
        }
    }

    private void CallEndgame()
    {
        if (endGameScreen != null)
        {
            endGameScreen.SetActive(true);
            Debug.Log("End game screen activated.");
        }
        else
        {
            Debug.LogError("End game screen is not assigned.");
        }
    }

    public void RespawnPlayer()
    {
        if (_currentPlayer != null)
        {
            Destroy(_currentPlayer.gameObject);
        }

        _currentPlayer = Instantiate(playerPrefab, startPoint.position, Quaternion.identity);

        if (_currentPlayer != null)
        {
            var playerController = _currentPlayer.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.healthBase.OnKill += OnPlayerKill;
            }
            else
            {
                Debug.LogError("PlayerController component not found on the player prefab.");
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate player prefab.");
        }
    }

    private void OnPlayerKill()
    {
        RespawnPlayer();
    }
}
