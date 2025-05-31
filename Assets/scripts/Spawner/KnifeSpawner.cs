using FruitCutting.Fruits;
using FruitCutting.KnifeObjects;
using System;
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
        private int _currentLevel = 1; // Текущий уровень

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            InitializeSpawnPoints();
        }

        private void InitializeSpawnPoints()
        {
            // Инициализация очереди точек спавна
            _spawnPointQueue = new Queue<Vector3>(_spawnPoints);
        }

        public void Initialize()
        {
            SpawnKnife();
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
            Debug.Log($"Current level set to: {_currentLevel}");
        }

        public void SpawnKnife()
        {
            if (_spawnPointQueue.Count == 0)
            {
                Debug.LogWarning("No spawn points available!");
                return;
            }

            if (_availableKnives.Count == 0)
            {
                Debug.LogWarning("No knives available to spawn!");
                return;
            }

            // Получаем индекс ножа на основе текущего уровня
            int knifeIndex = Mathf.Clamp(_currentLevel - 1, 0, _availableKnives.Count - 1);

            Vector3 spawnPosition = _spawnPointQueue.Dequeue();

            // Берем нож, соответствующий текущему уровню
            Knife knifeToSpawn = _availableKnives[knifeIndex];

            // Создаем экземпляр ножа в указанной точке
            Knife spawnedKnife = Instantiate(knifeToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}