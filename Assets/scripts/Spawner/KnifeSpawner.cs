using FruitCutting.Animation;
using FruitCutting.Cuonters;
using FruitCutting.KnifeObjects;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutting.Spawner
{
    public class KnifeSpawner : MonoBehaviour
    {
        public static KnifeSpawner Instance;

        [SerializeField] private List<Knife> _availableKnives = new List<Knife>();
        [SerializeField] private Vector3[] _spawnPoints;

        private Queue<Vector3> _spawnPointQueue;

        private int _baseCosts = 1;
        private int _maxCosts = 20;
        private int _maxLevel = 3;

        public float ÑurrentCosts { get; private set; }

        public int Level { get; private set; } = 1;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void Initialize()
        {
            UpdateCurrentCots();

            _spawnPointQueue = new Queue<Vector3>(_spawnPoints);

            SpawnKnife();
        }

        public void SpawnKnife()
        {
            if (_spawnPointQueue.Count == 0)
            {
                return;
            }

            if (_availableKnives.Count == 0)
            {
                return;
            }

            int knifeIndex = Mathf.Clamp(Level - 1, 0, _availableKnives.Count - 1);

            Vector3 spawnPosition = _spawnPointQueue.Dequeue();

            Knife knifeToSpawn = _availableKnives[knifeIndex];

            Knife spawnedKnife = Instantiate(knifeToSpawn, spawnPosition, knifeToSpawn.transform.rotation);

            KnifeAnimation.Instance.AddKnifeToQueue(spawnedKnife);
        }

        public void AddKnife()
        {
            if (Level < _maxLevel && Level > 0 && Counter.Instance.CountMoney >= ÑurrentCosts)
            {
                Level++;
                Counter.Instance.SpendMoney(ÑurrentCosts);
                UpdateCurrentCots();
                SpawnKnife();
            }
        }

        private void UpdateCurrentCots()
        {
            ÑurrentCosts = GetCurrentCosts();
        }

        private float GetCurrentCosts()
        {
            return _baseCosts + ((_maxCosts - _baseCosts) * (Level - 1)) / (_maxLevel - 1);
        }
    }
}