using System;
using TinyRTS.Core;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] BaseBuilding _currentBuilding;
        private Transform _currentBuildingPreview;
        private float _buildingOffset = 1f;

        private void Update()
        {
            if (_currentBuilding)
            {
                SelectBuilding();
            }

            if (_currentBuildingPreview)
            {
                float2 snappedPosition = BuildingGrid.Instance.GetTilePos(
                    Mathf.FloorToInt(WorldMouse.Instance.GetPosition().x ),
                    Mathf.FloorToInt(WorldMouse.Instance.GetPosition().z));
                _currentBuildingPreview.position = new Vector3(snappedPosition.x + _buildingOffset, 0f, snappedPosition.y + _buildingOffset);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Build();
            }
        }

        public void Build()
        {
            int startX = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().x);
            int startY = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().z);
            int width = _currentBuilding.BuildingData.width;
            int height = _currentBuilding.BuildingData.height;

            if (BuildingGrid.Instance.CanPlaceBuilding(startX, startY, width, height))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var tile = BuildingGrid.Instance.GetTile(startX + x, startY + y);
                        tile.SetOccupied(true);
                        Debug.Log($"Tile at {tile.Position} is {tile.IsOccupied}");
                    }
                }

                float2 placePosition = BuildingGrid.Instance.GetTilePos(startX, startY);

                Destroy(_currentBuildingPreview.gameObject);
                Instantiate(_currentBuilding.gameObject,
                    new Vector3(placePosition.x + _buildingOffset, 0f, placePosition.y + _buildingOffset),
                    Quaternion.identity);

                _currentBuildingPreview = null;
                // _currentBuilding = null;
            }
            else
            {
                Debug.Log("Cannot place building here.");
            }
        }

        public void SelectBuilding()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!_currentBuildingPreview)
                {
                    _currentBuildingPreview = Instantiate(_currentBuilding.BuildingData.previewPrefab,
                        WorldMouse.Instance.GetPosition(),
                        Quaternion.identity).transform;
                }
            }
        }
    }
}