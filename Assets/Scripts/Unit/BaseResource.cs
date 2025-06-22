using System;
using TinyRTS.BuildingSystem;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.Unit
{
    public class BaseResource : MonoBehaviour, IGatherable
    {
        [SerializeField] ResourceSO resourceData;

        private Collider _collider;
        private int _startingValue;
        private int _currentValue;

        public bool IsDepleted => _currentValue <= 0;


        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            if (!resourceData)
            {
                return;
            }

            _startingValue = resourceData.startingValue;
            _currentValue = _startingValue;
        }

        public void Gather()
        {
            _currentValue = math.clamp(_currentValue - 10, 0, _startingValue);
            if (IsDepleted)
            {
                gameObject.SetActive(false);
                BuildingGrid.Instance.UpdateGrid();
                Destroy(gameObject);
            }
        }
    }
}