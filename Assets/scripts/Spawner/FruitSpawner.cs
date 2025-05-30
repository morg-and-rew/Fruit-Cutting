using FruitCutting.BasketObject;
using FruitCutting.Fruits;
using FruitCutting.Tools.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutting.Spawner
{
    public class FruitSpawner : MonoBehaviour
    {
        [SerializeField] private List<Fruit> _availableFruit = new List<Fruit>();
        [SerializeField] private Vector3 _spawnPoint;

        private ObjectsPool<Fruit> _objectsPool;

        private bool _canSpawn;

        private Coroutine _spawnFruitCoroutine = null;

        private readonly uint InitFruitAmount = 0;

        private readonly float SpawnFrequency = 2f;

        public void Initialize()
        {
            _objectsPool = new ObjectsPool<Fruit>(Create, Add, Get, InitFruitAmount);

            _canSpawn = true;
        }

        private void FixedUpdate()
        {
            if (_canSpawn && _spawnFruitCoroutine == null) 
            {
                _spawnFruitCoroutine = StartCoroutine(SpawnFruits());
            }
        }

        private IEnumerator SpawnFruits()
        {
            yield return new WaitForSeconds(SpawnFrequency);

            if (_canSpawn)
            {
                _objectsPool.Get();
            }

            _spawnFruitCoroutine = null;
        }

        public void Add(Fruit fruit)
        {
            fruit.Disactivate();
        }

        public void Get(Fruit fruit)
        {
            fruit.Initialize(_spawnPoint, () => _objectsPool.Add(fruit));
            Basket.Instance.AddFruit(fruit);
        }

        private Fruit Create()
        {
            return Instantiate(_availableFruit[UnityEngine.Random.Range(0, _availableFruit.Count)]);
        }

    }
}
