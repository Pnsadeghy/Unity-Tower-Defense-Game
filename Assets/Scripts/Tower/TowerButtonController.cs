using System;
using System.Collections;
using System.Collections.Generic;
using Control;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tower
{
    public class TowerButtonController : MonoBehaviour
    {
        public TowerController tower;

        private Button _button;

        private void Start()
        {
            TryGetComponent(out _button);
        }

        public void OnClick()
        {
            if (UIController.Instance.chosenTower == tower)
            {
                UIController.Instance.chosenTower = null;
            }
            else
            {
                UIController.Instance.chosenTower = tower;

            }
        }

        private void Update()
        {

        }
    }
}
