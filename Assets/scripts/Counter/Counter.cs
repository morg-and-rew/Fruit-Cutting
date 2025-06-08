using FruitCutting.BasketObject;
using FruitCutting.Fruits;
using System;
using UnityEngine;
using TMPro;

namespace FruitCutting.Cuonters
{
    public class Counter : MonoBehaviour
    {
        public static Counter Instance;

        [SerializeField] private TextMeshProUGUI _textCountMoney;

        private float _initialMoney = 100f;

        public float CountMoney { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            CountMoney = _initialMoney;
            UpdateMoneyText();
        }

        public void AddMoney(float amount)
        {
            if (amount > 0)
            {
                CountMoney += amount;
                UpdateMoneyText();
            }
        }

        public void SpendMoney(float amount)
        {
            if (amount > 0 && CountMoney >= amount)
            {
                CountMoney -= amount;
                UpdateMoneyText();
            }
        }

        private void UpdateMoneyText()
        {
            if (_textCountMoney != null)
            {
                _textCountMoney.text = $"{CountMoney}";
            }
        }
    }
}