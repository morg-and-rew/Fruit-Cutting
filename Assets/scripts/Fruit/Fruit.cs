using FruitCutting.CharactersActions;
using System;
using System.Collections;
using UnityEngine;

namespace FruitCutting.Fruits
{
    public class Fruit : MonoBehaviour
    {
        [SerializeField] private FruitParamets _dragonFruitParamets;

        private MovementAction _movementAction = new MovementAction();
        private Coroutine _timeEventCoroutine;

        private float _speed = 1f;

        public Material CrossSelectionMaterial { get; private set; }
        public float Price { get; private set; }

        public void Initialize(Vector3 position, Action die)
        {
            gameObject.transform.position = position;

            CrossSelectionMaterial = _dragonFruitParamets.fruitSliceMaterial;

            Price = _dragonFruitParamets.price;
        }

        public void Initialize(Material crossSelectionMaterial, float price)
        {
            CrossSelectionMaterial = crossSelectionMaterial;
            Price = price;
        }

        private void FixedUpdate()
        {
            _movementAction.FixedUpdateMove(gameObject.transform, Vector3.right, _speed);
        }

        public void Disactivate()
        {
            if (_timeEventCoroutine != null)
            {
                StopCoroutine(_timeEventCoroutine);
            }
        }
    }
}