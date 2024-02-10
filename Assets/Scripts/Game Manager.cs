using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plataforma2D.Singleton;
using DG.Tweening;
using System.Threading;
using Unity.VisualScripting;

public class GameManager : Plataforma2D.Singleton.Singleton<GameManager>
{
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform startPoint;

    private GameObject _currentPlayer;
    private Transform _playerPrefab;

    [Header("Animation")]
    public float duration = .5f;
    public float delay = 1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position = startPoint.transform.position;
        _currentPlayer.transform.DOScale(0, duration).SetEase(ease).From().SetDelay(delay);
        _currentPlayer.AddComponent<PlayerController>();

    }

    public void GrowPlayerMethod()
    {
        _currentPlayer.GetComponent<PlayerController>().StartCoroutine(GrowPlayer());
    }

    IEnumerator GrowPlayer()
    {
        _currentPlayer.transform.DOScale(2, duration).SetEase(ease);
        yield return new WaitForSeconds(3);
        _currentPlayer.transform.DOScale(1, duration).SetEase(ease);
    }

}
