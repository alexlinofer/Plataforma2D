using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plataforma2D.Singleton;
using TMPro;


public class CollectableManager : Singleton<CollectableManager>
{
    public SOInt coins;
    public SOInt satellites;
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextSatellites;


    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        satellites.value = 0;
        coins.value = 0;
        UpdateUI();

    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        UpdateUI();
    }
    public void AddSatellites(int amount = 1)
    {
        satellites.value += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //uiTextCoins.text = coins.ToString();
        //UIInGameManager.UpdateTextCoins(coins.value.ToString());
    }

}
