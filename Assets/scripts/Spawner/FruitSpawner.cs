using FruitCutting.BasketObject;
using FruitCutting.Cuonters;
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
        public static FruitSpawner Instance;

        [SerializeField] private List<Fruit> _availableFruit = new List<Fruit>();
        [SerializeField] private Vector3 _spawnPoint;
        [SerializeField] private List<FruitParamets> _fruitParametsList = new List<FruitParamets>();

        private ObjectsPool<Fruit> _objectsPool;

        private bool _canSpawn;

        private Coroutine _spawnFruitCoroutine = null;

        private readonly uint InitFruitAmount = 0;

        private readonly float SpawnFrequency = 5f;

        private int _baseCosts = 1;
        private int _maxCosts = 20;
        private int _maxLevel = 20;

        public float CurrentCosts { get; private set; }

        public int Level { get; private set; } = 1 ;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            UpdateCurrentCots();
        }

        public void Initialize()
        {
            SortFruitsByPrice();

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
            Basket.Instance.AddToFruitQueue(fruit);
        }

        private Fruit Create()
        {
            List<float> weights = CalculateWeights();

            int randomIndex = WeightedRandom(weights);

            Fruit newFruit = Instantiate(_availableFruit[randomIndex]);
            newFruit.Initialize(GetMaterialFromParamets(randomIndex), GetPriceFromParamets(randomIndex));

            return newFruit;
        }

        private List<float> CalculateWeights()
        {
            List<float> weights = new List<float>();

            int availableFruitsCount = GetAvailableFruitsCount();

            float alpha = Mathf.Lerp(0.5f, 2f, (float)Level / _maxLevel);

            for (int i = 0; i < availableFruitsCount; i++)
            {
                float price = GetPriceFromParamets(i);
                float weight = Mathf.Pow(price, alpha); 
                weights.Add(weight);
            }

            return NormalizeWeights(weights);
        }

        private int GetAvailableFruitsCount()
        {
            int fruitCount = _fruitParametsList.Count;

            if (fruitCount == 1)
            {
                return 1; 
            }

            for (int i = 0; i < fruitCount; i++)
            {
                int unlockLevel = Mathf.FloorToInt((i * (_maxLevel - 1)) / (float)(fruitCount - 1)) + 1;
                if (Level < unlockLevel)
                {
                    return i;
                }
            }

            return fruitCount;
        }

        private List<float> NormalizeWeights(List<float> weights)
        {
            float totalWeight = 0f;

            foreach (float weight in weights)
            {
                totalWeight += weight;
            }

            if (totalWeight == 0)
            {
                return weights;
            }

            List<float> normalizedWeights = new List<float>();

            foreach (float weight in weights)
            {
                normalizedWeights.Add(weight / totalWeight);
            }

            return normalizedWeights;
        }

        private int WeightedRandom(List<float> weights)
        {
            float totalWeight = 0f;

            foreach (float weight in weights)
            {
                totalWeight += weight;
            }

            if (totalWeight == 0)
            {
                return 0;
            }

            float randomValue = UnityEngine.Random.Range(0f, totalWeight);

            float cumulativeWeight = 0f;

            for (int i = 0; i < weights.Count; i++)
            {
                cumulativeWeight += weights[i];

                if (randomValue <= cumulativeWeight)
                {
                    return i;
                }
            }

            return weights.Count - 1;
        }

        private void SortFruitsByPrice()
        {
            _fruitParametsList.Sort((param1, param2) => param1.price.CompareTo(param2.price));
        }

        private float GetPriceFromParamets(int index)
        {
            if (index >= 0 && index < _fruitParametsList.Count)
            {
                return _fruitParametsList[index].price;
            }

            return 0f;
        }

        private Material GetMaterialFromParamets(int index)
        {
            if (index >= 0 && index < _fruitParametsList.Count)
            {
                return _fruitParametsList[index].fruitSliceMaterial;
            }

            return null;
        }

        public void IncreaseLevel()
        {
            if (Level < _maxLevel && Level > 0 && Counter.Instance.CountMoney >= CurrentCosts)
            {
                Level++;
                Counter.Instance.SpendMoney(CurrentCosts);
                UpdateCurrentCots();
            }
        }

        private void UpdateCurrentCots()
        {
            CurrentCosts = GetCurrentCosts();
        }
        private float GetCurrentCosts()
        {
            return _baseCosts + ((_maxCosts - _baseCosts) * (Level - 1)) / (_maxLevel - 1);
        }
    }
}