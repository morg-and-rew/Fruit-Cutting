using UnityEngine;
using UnityEngine.UI;

namespace FruitCutting.Canvas.Game
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Button _startButton; // Поле для кнопки

        private void OnEnable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.AddListener(OnStartButtonClicked); // Подписываемся на событие нажатия кнопки
            }
        }

        private void OnDisable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(OnStartButtonClicked); // Отписываемся от события
            }
        }
    }
}