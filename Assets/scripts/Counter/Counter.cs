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
        private float _initialMoney = 0f;

        private float _countMoney;

        public event Action<float> OnCountMoneyChanged;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            _countMoney = _initialMoney;
            UpdateMoneyText();
        }

        private void OnEnable()
        {
            OnCountMoneyChanged += HandleMoneyChange;
        }

        private void OnDisable()
        {
            OnCountMoneyChanged -= HandleMoneyChange;
        }

        public void AddMoney(float amount)
        {
            if (amount > 0)
            {
                _countMoney += amount;
                OnCountMoneyChanged?.Invoke(_countMoney);
            }
        }

        public bool SpendMoney(float amount)
        {
            if (amount > 0 && _countMoney >= amount)
            {
                _countMoney -= amount;
                OnCountMoneyChanged?.Invoke(_countMoney);
                return true;
            }

            return false;
        }

        private void HandleMoneyChange(float newAmount)
        {
            UpdateMoneyText();
        }

        private void UpdateMoneyText()
        {
            if (_textCountMoney != null)
            {
                _textCountMoney.text = $"{_countMoney}";
            }
        }
    }
}