using EzySlice;
using UnityEngine;
using FruitCutting.Fruits;
using FruitCutting.BasketObject;
using FruitCutting.Cuonters;

namespace FruitCutting.Slice
{
    public class SliceObject : MonoBehaviour
    {
        [SerializeField] private Transform _planeDebug;
        private float _upwardForce = 1f; 
        private float _scatterForce = 1f;

        public void Slice(Fruit target, Material crossSelectionMaterial)
        {
            SlicedHull hull = target.gameObject.Slice(_planeDebug.position, _planeDebug.right);

            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(target.gameObject, crossSelectionMaterial);
                SetupSlicedComponent(upperHull, Vector3.up, target); 

                GameObject lowerHull = hull.CreateLowerHull(target.gameObject, crossSelectionMaterial);
                SetupSlicedComponent(lowerHull, Vector3.up, target); 

                Destroy(target.gameObject);              
            }
        }

        private void SetupSlicedComponent(GameObject slicedObject, Vector3 baseDirection, Fruit target)
        {
            if (slicedObject == null)
                return;

            MeshCollider collider = slicedObject.GetComponent<MeshCollider>();

            if (collider == null)
            {
                collider = slicedObject.AddComponent<MeshCollider>();
            }

            Rigidbody rigidbody = slicedObject.GetComponent<Rigidbody>();

            if (rigidbody == null)
            {
                rigidbody = slicedObject.AddComponent<Rigidbody>();
            }

            Fruit fruit = slicedObject.GetComponent<Fruit>();

            if (fruit == null)
            {
                fruit = slicedObject.AddComponent<Fruit>();
            }

            fruit.Initialize(target.CrossSelectionMaterial, target.Price);

            Basket.Instance.AddToFruitQueue(fruit);

            collider.convex = true;
            rigidbody.useGravity = true;
            rigidbody.mass = 1f;
            rigidbody.linearDamping = 0.1f;
            rigidbody.angularDamping = 0.1f;
            rigidbody.freezeRotation = true;

            Vector3 scatterDirection;

            do
            {
                scatterDirection = new Vector3(
                    GetNonZeroRandom(-2f, 2f), 
                    GetNonZeroRandom(0.5f, 2f),
                    GetNonZeroRandom(-2f, 2f) 
                );
            } while (scatterDirection == Vector3.zero);

            Vector3 forceDirection = (baseDirection + scatterDirection * _scatterForce).normalized;
            rigidbody.AddForce(forceDirection * _upwardForce, ForceMode.Impulse);
        }

        private float GetNonZeroRandom(float min, float max)
        {
            float value;
            do
            {
                value = Random.Range(min, max);
            } while (Mathf.Approximately(value, 0f));

            return value;
        }
    }
}