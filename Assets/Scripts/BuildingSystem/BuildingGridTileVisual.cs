using System;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class BuildingGridTileVisual : MonoBehaviour
    {
        private BuildingGridTile tile;
        private MeshRenderer _renderer;
        private float offset = 0.5f;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(float2 position)
        {
            tile = BuildingGrid.Instance.GetTile((int)position.x, (int)position.y);
            if (tile != null)
            {
                tile.OnOccupiedChanged += UpdateVisual;
                UpdateVisual(tile.IsOccupied);
            }
            transform.position = new Vector3(position.x + offset, 0.09f, position.y + offset);
        }

        private void OnDestroy()
        {
            if (tile != null)
                tile.OnOccupiedChanged -= UpdateVisual;
        }

        private void UpdateVisual(bool isOccupied)
        {
            _renderer.material.color = isOccupied ? Color.red : Color.green;
        }
    }
}