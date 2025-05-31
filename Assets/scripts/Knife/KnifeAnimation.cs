using System;
using System.Collections;
using UnityEngine;

namespace FruitCutting.Animation
{
    public class KnifeAnimation : MonoBehaviour
    {
        public event Action OnChangeSpeed;

        private float _baseRotationSpeed = 1000; 
        private float _currentRotationSpeed;  

        private Quaternion _targetRotationDown = Quaternion.Euler(0, 0, 0);  
        private Quaternion _targetRotationUp = Quaternion.Euler(-90, 0, 0);  

        private bool isRotatingDown = true;

        private void OnEnable()
        {
            OnChangeSpeed += SetRotationSpeed;
        }

        private void OnDisable()
        {
            OnChangeSpeed -= SetRotationSpeed;
        }

        private void Start()
        {
            _currentRotationSpeed = _baseRotationSpeed; 
            StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
            while (true)
            {
                Quaternion targetRotation = isRotatingDown ? _targetRotationDown : _targetRotationUp;

                while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
                {
                    float angleToTarget = Quaternion.Angle(transform.rotation, targetRotation);
                    float progress = 1 - (angleToTarget / 90f); 

                    if (!isRotatingDown)
                    {
                        _currentRotationSpeed = Mathf.Lerp(_baseRotationSpeed, 10f, progress);
                    }
                    else
                    {
                        _currentRotationSpeed = Mathf.Lerp(10f, _baseRotationSpeed, progress);
                    }

                    transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        targetRotation,
                        _currentRotationSpeed * Time.deltaTime
                    );

                    yield return null;
                }

                isRotatingDown = !isRotatingDown;

                yield return null;
            }
        }

        public void SetRotationSpeed()
        {
            OnChangeSpeed?.Invoke();
        }
    }
}