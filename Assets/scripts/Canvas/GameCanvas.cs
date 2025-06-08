using FruitCutting.Animation;
using FruitCutting.Spawner;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

namespace FruitCutting.Canvas.Game
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Button _knifeButtonSpawner;
        [SerializeField] private Button _knifeButtonSpeed;
        [SerializeField] private Button _fruitButtonRarely;

        [SerializeField] private TMP_Text _knifeSpawnerLevelText;
        [SerializeField] private TMP_Text _knifeSpawnerCostText;

        [SerializeField] private TMP_Text _knifeSpeedLevelText;
        [SerializeField] private TMP_Text _knifeSpeedCostText;

        [SerializeField] private TMP_Text _fruitRarelyLevelText;
        [SerializeField] private TMP_Text _fruitRarelyCostText;

        private void Start()
        {
            if (_knifeButtonSpawner != null && _knifeButtonSpeed != null && _fruitButtonRarely != null)
            {
                _knifeButtonSpawner.onClick.AddListener(() =>
                {
                    KnifeSpawner.Instance.AddKnife();
                    UpdateKnifeSpawnerInfo();
                });

                _knifeButtonSpeed.onClick.AddListener(() =>
                {
                    KnifeAnimation.Instance.AddSpeed();
                    UpdateKnifeSpeedInfo();
                });

                _fruitButtonRarely.onClick.AddListener(() =>
                {
                    FruitSpawner.Instance.IncreaseLevel();
                    UpdateFruitRarelyInfo();
                });
            }

            UpdateKnifeSpawnerInfo();
            UpdateKnifeSpeedInfo();
            UpdateFruitRarelyInfo();
        }

        private void OnDisable()
        {
            if (_knifeButtonSpawner != null && _knifeButtonSpeed != null && _fruitButtonRarely != null)
            {
                _knifeButtonSpawner.onClick.RemoveAllListeners();
                _knifeButtonSpeed.onClick.RemoveAllListeners();
                _fruitButtonRarely.onClick.RemoveAllListeners();
            }
        }

        private void UpdateKnifeSpawnerInfo()
        {
            if (_knifeSpawnerLevelText != null && _knifeSpawnerCostText != null)
            {
                _knifeSpawnerLevelText.text = $"{KnifeSpawner.Instance.Level}";
                _knifeSpawnerCostText.text = $"{KnifeSpawner.Instance.ÑurrentCosts}";
            }
        }

        private void UpdateKnifeSpeedInfo()
        {
            if (_knifeSpeedLevelText != null && _knifeSpeedCostText != null)
            {
                _knifeSpeedLevelText.text = $"{KnifeAnimation.Instance.Level}";
                _knifeSpeedCostText.text = $"{KnifeAnimation.Instance.CurrentCosts}";
            }
        }

        private void UpdateFruitRarelyInfo()
        {
            if (_fruitRarelyLevelText != null && _fruitRarelyCostText != null)
            {
                _fruitRarelyLevelText.text = $"{FruitSpawner.Instance.Level}";
                _fruitRarelyCostText.text = $"{FruitSpawner.Instance.CurrentCosts}";
            }
        }
    }
}