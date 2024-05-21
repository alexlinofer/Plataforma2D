using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextToTimer : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI textMeshPro;

    private void OnEnable()
    {
        textMeshPro.text = gameManager.timer.ToString("F2");
    }
}
