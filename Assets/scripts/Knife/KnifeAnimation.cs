using FruitCutting.Cuonters;
using FruitCutting.KnifeObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutting.Animation
{
    public class KnifeAnimation : MonoBehaviour
    {
        public static KnifeAnimation Instance;

        private List<Knife> _knives = new List<Knife>(); 

        private int _baseRotationSpeed = 1000;
        private int _maxRotationSpeed = 5000;
        private float _currentRotationSpeed;

        private int _baseCosts = 1;
        private int _maxCosts = 20;
        private int _maxLevel = 20;
        public float CurrentCosts { get; private set; }

        public int Level { get; private set; } = 1;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            UpdateCurrentSpeed();
            UpdateCurrentCots();
        }

        public void AddKnifeToQueue(Knife knife)
        {
            _knives.Add(knife); 
            StartCoroutine(RotateKnife(knife, _knives.Count - 1)); 
        }

        private IEnumerator RotateKnife(Knife knife, int index)
        {
            Quaternion targetRotationDown = Quaternion.Euler(0, 0, 0); 
            Quaternion targetRotationUp = Quaternion.Euler(-90, 0, 0); 

            bool isRotatingDown = true;

            while (true)
            {
                Quaternion targetRotation = isRotatingDown ? targetRotationDown : targetRotationUp;

                if (isRotatingDown)
                {
                    yield return StartCoroutine(SmallAmplitudeMovement(knife, index));
                }

                while (Quaternion.Angle(knife.transform.rotation, targetRotation) > 0.1f)
                {
                    float angleToTarget = Quaternion.Angle(knife.transform.rotation, targetRotation);
                    float progress = 1 - (angleToTarget / 90f);

                    float sharpnessDistance = 10f; 
                    float speed = GetCurrentSpeed();

                    if (angleToTarget <= sharpnessDistance)
                    {
                        speed = _maxRotationSpeed; 
                    }
                    else
                    {
                        speed = Mathf.Lerp(10f, GetCurrentSpeed(), progress); 
                    }

                    knife.transform.rotation = Quaternion.RotateTowards(
                        knife.transform.rotation,
                        targetRotation,
                        speed * Time.deltaTime
                    );

                    yield return null;
                }

                isRotatingDown = !isRotatingDown;
            }
        }

        private IEnumerator SmallAmplitudeMovement(Knife knife, int index)
        {
            float baseDuration = 1f;

            float frequency = 5f;
            float amplitude = 5f; 

            float timer = 0f;
            Quaternion initialRotation = knife.transform.rotation;

            if (index > 0)
            {
                Knife previousKnife = _knives[index - 1];
                Quaternion previousTargetRotation = Quaternion.Euler(0, 0, 0);

                while (Quaternion.Angle(previousKnife.transform.rotation, previousTargetRotation) > 20f)
                {
                    yield return null;
                }
            }

            while (timer < baseDuration)
            {
                float oscillation = Mathf.Sin(timer * frequency) * amplitude;
                knife.transform.rotation = initialRotation * Quaternion.Euler(oscillation, 0, 0);

                timer += Time.deltaTime;
                yield return null;
            }

            knife.transform.rotation = initialRotation;
        }

        public void AddSpeed()
        {
            if (Level < _maxLevel && Level > 0 && Counter.Instance.CountMoney >= CurrentCosts)
            {
                Level++;
                Counter.Instance.SpendMoney(CurrentCosts);
                UpdateCurrentSpeed();
                UpdateCurrentCots();
            }
        }

        private void UpdateCurrentSpeed()
        {
            _currentRotationSpeed = GetCurrentSpeed();
        }

        private void UpdateCurrentCots()
        {
            CurrentCosts = GetCurrentCosts();
        }

        private float GetCurrentSpeed()
        {
            return _baseRotationSpeed + ((_maxRotationSpeed - _baseRotationSpeed) * (Level - 1)) / (_maxLevel - 1);
        }

        private float GetCurrentCosts()
        {
            return _baseCosts + ((_maxCosts - _baseCosts) * (Level - 1)) / (_maxLevel - 1);
        }
    }
}