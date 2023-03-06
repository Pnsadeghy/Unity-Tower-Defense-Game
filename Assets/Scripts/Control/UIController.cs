using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Control
{
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
        public RectTransform buttonContainer;
        public List<TowerController> towers;
        public Button towerButton;
        public TowerController chosenTower = null;

        private float _currentCoins;
        private float _waveSec;
        private List<TowerButtonController> buttons;

        private void Start()
        {
            buttons = new List<TowerButtonController>();
            buttonContainer.sizeDelta = new Vector2(towers.Count * 130, buttonContainer.sizeDelta.y);
            buttonContainer.anchoredPosition = new Vector2(buttonContainer.sizeDelta.x / -2, 0);
            var parent = buttonContainer.transform.parent.GetComponent<RectTransform>();
            parent.sizeDelta = new Vector2(buttonContainer.sizeDelta.x + 220, parent.sizeDelta.y);

            var pos = 0;
            foreach (var tower in towers)
            {
                var button = Instantiate(towerButton, buttonContainer);
                var buttonRect = button.GetComponent<RectTransform>();
                buttonRect.anchoredPosition = new Vector2(-pos - 70, 0);
                pos += 130;
                button.transform.GetChild(0).GetComponent<Image>().sprite = tower.sprite;
                var controller = button.GetComponent<TowerButtonController>();
                controller.tower = tower;
                buttons.Add(controller);
            }
            
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
}