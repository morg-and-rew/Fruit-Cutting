using FruitCutting.Spawner;
using UnityEngine;

namespace FruitCutting.Game
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private FruitSpawner _fruitSpawner;
        [SerializeField] private KnifeSpawner _knifeSpawner;

        private void Start()
        {
            _fruitSpawner.Initialize();
            _knifeSpawner.Initialize();
        }
    }
}
