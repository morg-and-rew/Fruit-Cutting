using FruitCutting.Spawner;
using UnityEngine;

namespace FruitCutting.Game
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private FruitSpawner _fruitSpawner;

        private void Start()
        {
            _fruitSpawner.Initialize();
        }
    }
}
