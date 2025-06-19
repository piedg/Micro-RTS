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

        private void Update()
        {
            if (_currentBuilding)
            {
                SelectBuilding();
            }

            if (_currentBuildingPreview)
            {
                _currentBuildingPreview.position = WorldMouse.Instance.GetPosition();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Build();
            }
        }

        public void Build()
        {
            if (BuildingGrid.Instance.CanPlaceBuilding(
                    (int)WorldMouse.Instance.GetPosition().x,
                    (int)WorldMouse.Instance.GetPosition().y,
                    _currentBuilding.BuildingData.width,
                    _currentBuilding.BuildingData.height))
            {
                Destroy(_currentBuildingPreview.gameObject);
                Instantiate(_currentBuilding.gameObject,
                    WorldMouse.Instance.GetPosition(),
                    Quaternion.identity);

                for (int i = 0; i < _currentBuilding.BuildingData.width; i++)
                {
                    for (int j = 0; j < _currentBuilding.BuildingData.height; j++)
                    {
                        BuildingGrid.Instance.SetTileOccupied(i, j, true);
                    }
                }


                BuildingGrid.Instance.UpdateGrid();
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