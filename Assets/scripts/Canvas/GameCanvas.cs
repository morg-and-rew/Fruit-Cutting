
using FruitCutting.Spawner;
using UnityEngine;
using UnityEngine.UI;

namespace FruitCutting.Canvas.Game
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Button _knifeButtonSpawner;

        private void OnEnable()
        {
            if (_knifeButtonSpawner != null)
            {
                _knifeButtonSpawner.onClick.AddListener(KnifeSpawner.Instance.SpawnKnife);
            }
        }

        private void OnDisable()
        {
            if (_knifeButtonSpawner != null)
            {
                _knifeButtonSpawner.onClick.RemoveListener(KnifeSpawner.Instance.SpawnKnife); 
            }
        }
    }
}