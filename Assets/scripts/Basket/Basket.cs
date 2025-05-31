using FruitCutting.Fruits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutting.BasketObject
{
    public class Basket : MonoBehaviour
    {
        public static Basket Instance;

        [SerializeField] private Queue<Fruit> _activeFruits = new Queue<Fruit>();

        private float _timeBeforeRemove = 5f; 

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            StartCoroutine(ProcessQueue());
        }

        public void AddToFruitQueue(Fruit fruit)
        {
            if (fruit != null)
            {
                _activeFruits.Enqueue(fruit);
            }
        }

        public Queue<Fruit> GetActiveFruits()
        {
            return _activeFruits;
        }

        public void ClearActiveFruits()
        {
            while (_activeFruits.Count > 0)
            {
                Destroy(_activeFruits.Dequeue().gameObject);
            }
        }

        private IEnumerator ProcessQueue()
        {
            while (true)
            {
                if (_activeFruits.Count > 6)
                {
                    yield return new WaitForSeconds(_timeBeforeRemove);

                    Fruit fruit = _activeFruits.Dequeue();

                    if (fruit != null)
                    {
                        Destroy(fruit.gameObject); 
                    }
                }
                else
                {
                    yield return null; 
                }
            }
        }
    }
}