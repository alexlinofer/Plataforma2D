using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinCounter;
    public CollectableManager collectableManager;

    private void Update()
    {
        coinCounter.text = "x " + collectableManager.coins.ToString();
    }
}
