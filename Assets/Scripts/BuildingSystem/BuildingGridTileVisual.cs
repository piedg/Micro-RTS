using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    public class BuildingGridTileVisual : MonoBehaviour
    {
        private BuildingGridTile _tile;
        private MeshRenderer _renderer;
        private readonly float _offset = 0.5f;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(float2 position)
        {
            _tile = BuildingGrid.Instance.GetTile((int)position.x, (int)position.y);
            if (_tile != null)
            {
                _tile.OnOccupiedChanged += UpdateVisual;
                UpdateVisual(_tile.IsOccupied);
            }

            transform.position = new Vector3(position.x + _offset, 0.09f, position.y + _offset);
        }

        private void OnDestroy()
        {
            _tile.OnOccupiedChanged -= UpdateVisual;
        }

        private void UpdateVisual(bool isOccupied)
        {
            _renderer.material.color = isOccupied ? Color.red : Color.green;
        }
    }
}