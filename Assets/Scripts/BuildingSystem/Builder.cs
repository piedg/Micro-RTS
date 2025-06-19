using TinyRTS.Core;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] BaseBuilding _currentBuilding;
        private Transform _currentBuildingPreview;
        private const float BUILDING_POS_OFFSET = 1f;

        private void Update()
        {
            if (_currentBuilding)
            {
                SelectBuilding();
            }

            if (_currentBuildingPreview)
            {
                var snappedPosition = BuildingGrid.Instance.GetTilePos(
                    Mathf.FloorToInt(WorldMouse.Instance.GetPosition().x ),
                    Mathf.FloorToInt(WorldMouse.Instance.GetPosition().z));
                
                _currentBuildingPreview.position = new Vector3(
                    snappedPosition.x + BUILDING_POS_OFFSET, 
                    0f, 
                    snappedPosition.y + BUILDING_POS_OFFSET);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Build();
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                CancelBuildingMode();
            }
        }

        private void CancelBuildingMode()
        {
            if (_currentBuildingPreview)
            {
                Destroy(_currentBuildingPreview.gameObject);
                _currentBuildingPreview = null;
            }

            BuildingGrid.Instance.HideTiles();
            _currentBuilding = null;
        }

        public void Build()
        {
            var startX = (int)math.floor(WorldMouse.Instance.GetPosition().x);
            var startY = (int)math.floor(WorldMouse.Instance.GetPosition().z);
            
            var width = _currentBuilding.BuildingData.width;
            var height = _currentBuilding.BuildingData.height;

            if (BuildingGrid.Instance.CanPlaceBuilding(startX, startY, width, height))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var tile = BuildingGrid.Instance.GetTile(startX + x, startY + y);
                        tile.SetOccupied(true);
                    }
                }

                var placePosition = BuildingGrid.Instance.GetTilePos(startX, startY);

                Destroy(_currentBuildingPreview.gameObject);
                
                GameObject buildingInstance = Instantiate(_currentBuilding.gameObject,
                    new Vector3(
                        placePosition.x + BUILDING_POS_OFFSET, 
                        0f, 
                        placePosition.y + BUILDING_POS_OFFSET),
                    Quaternion.identity);
                
                
                BuildingGrid.Instance.HideTiles();
                _currentBuildingPreview = null;
                //_currentBuilding = null;
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
                    BuildingGrid.Instance.ShowTiles();
                    _currentBuildingPreview = Instantiate(_currentBuilding.BuildingData.previewPrefab,
                        WorldMouse.Instance.GetPosition(),
                        Quaternion.identity).transform;
                }
            }
        }
    }
}