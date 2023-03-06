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

    private void Update()
    {
        var time = "00:00";
        if (_waveSec > 0)
        {
            _waveSec -= Time.deltaTime;
            if (_waveSec > 0)
            {
                var minutes = Mathf.FloorToInt(_waveSec / 60f);
                var seconds = Mathf.FloorToInt(_waveSec % 60f);
                
                time = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        timeText.text = time;
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

    public bool CheckCoin(float value) => _currentCoins >= value;

    public bool DecCoin(float value)
    {
        if (_currentCoins < value) return false;
        _currentCoins -= value;
        SetCoinText();
        return true;
    }

    private void SetCoinText()
    {
        coinText.text = "$ " + _currentCoins;
    }
}
