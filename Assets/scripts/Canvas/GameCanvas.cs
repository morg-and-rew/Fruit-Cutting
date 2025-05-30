using UnityEngine;
using UnityEngine.UI;

namespace FruitCutting.Canvas.Game
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Button _startButton; // ���� ��� ������

        private void OnEnable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.AddListener(OnStartButtonClicked); // ������������� �� ������� ������� ������
            }
        }

        private void OnDisable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(OnStartButtonClicked); // ������������ �� �������
            }
        }
    }
}