using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public UIController()
    {
        Instance = this;
    }

    public float coins;
    public Text coinText;
    public Text timeText;

    private float _currentCoins;
    private float _waveSec;

    private void Start()
    {
        _currentCoins = coins;
        SetCoinText();
    }

    public void SetTimer(float sec)
    {
        _waveSec = sec;
    }

    public void AddCoin(float value)
    {
        _currentCoins += value;
        SetCoinText();
    }

    private void SetCoinText()
    {
        coinText.text = "$ " + _currentCoins;
    }
}
