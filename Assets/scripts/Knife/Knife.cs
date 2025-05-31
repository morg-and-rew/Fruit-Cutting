using FruitCutting.BasketObject;
using FruitCutting.Cuonters;
using FruitCutting.Fruits;
using FruitCutting.Slice;
using FruitCutting.Spawner;
using System;
using UnityEngine;

namespace FruitCutting.KnifeObjects
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] SliceObject _sliceObject;

        private bool _isSlice = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Fruit fruit) && _isSlice)
            {
                _sliceObject.Slice(fruit, fruit.CrossSelectionMaterial);
                _isSlice = false;
                Counter.Instance.AddMoney(fruit.Price);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Fruit fruit))
            {
                _isSlice = true;
            }
        }
    }
}
