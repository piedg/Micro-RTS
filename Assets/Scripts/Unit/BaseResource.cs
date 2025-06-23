using TinyRTS.BuildingSystem;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.Unit
{
    public class BaseResource : MonoBehaviour, IGatherable
    {
        [SerializeField] ResourceSO resourceData;

        private int _startingValue;
        private int _currentValue;

        int _width => resourceData ? resourceData.width : 1;
        int _height => resourceData ? resourceData.height : 1;

        public bool IsDepleted => _currentValue <= 0;

        private void Start()
        {
            if (!resourceData)
            {
                return;
            }

            var gridX = (int)math.floor(transform.position.x);
            var gridY = (int)math.floor(transform.position.z);

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var tile = BuildingGrid.Instance.GetTile(gridX + x, gridY + y);
                    tile.SetOccupied(true);
                }
            }

            _startingValue = resourceData.startingValue;
            _currentValue = _startingValue;
        }

        public void Gather()
        {
            _currentValue = math.clamp(_currentValue - 10, 0, _startingValue);
            if (IsDepleted)
            {
                ClearOccupiedTiles();
                Destroy(gameObject);
            }
        }

        private void ClearOccupiedTiles()
        {
            BuildingGrid.Instance.ClearTiles((int)transform.position.x, (int)transform.position.z, _width, _height);
        }
    }
}