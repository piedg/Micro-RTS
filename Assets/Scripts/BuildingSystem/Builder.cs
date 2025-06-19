using TinyRTS.Core;
using TinyRTS.Inputs;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] BaseBuilding _currentBuilding;
        [SerializeField] BaseBuilding barrack;
        [SerializeField] BaseBuilding hq;
        [SerializeField] BaseBuilding treeFactory;

        private Transform _currentBuildingPreview;
        private const float BUILDING_POS_OFFSET = 0f;

        bool isBuildingModeOn = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CancelBuildingMode();
                isBuildingModeOn = true;
                _currentBuilding = hq;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CancelBuildingMode();
                isBuildingModeOn = true;
                _currentBuilding = barrack;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CancelBuildingMode();
                isBuildingModeOn = true;
                _currentBuilding = treeFactory;
            }

            if (isBuildingModeOn)
            {
                if (_currentBuilding)
                {
                    SelectBuilding(_currentBuilding);
                }

                if (_currentBuildingPreview)
                {
                    var gridX = math.floor(WorldMouse.Instance.GetPosition().x);
                    var gridY = math.floor(WorldMouse.Instance.GetPosition().z);
                    var snappedPosition = BuildingGrid.Instance.GetTilePos((int)gridX, (int)gridY);
                    var width = _currentBuilding.BuildingData.width;
                    var height = _currentBuilding.BuildingData.height;
                    _currentBuildingPreview.position = new Vector3(
                        snappedPosition.x + (width / 2),
                        0f,
                        snappedPosition.y + (width / 2));
                }

                if (InputManager.Instance.IsMouseLeftButtonDown())
                {
                    Build();
                }

                if (InputManager.Instance.IsMouseRightButton())
                {
                    CancelBuildingMode();
                }
            }
        }

        private void CancelBuildingMode()
        {
            isBuildingModeOn = false;
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
            int gridX = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().x);
            int gridY = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().z);

            var width = _currentBuilding.BuildingData.width;
            var height = _currentBuilding.BuildingData.height;

            if (BuildingGrid.Instance.CanPlaceBuilding(gridX, gridY, width, height))
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var tile = BuildingGrid.Instance.GetTile(gridX + x, gridY + y);
                        tile.SetOccupied(true);
                    }
                }

                var placeX = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().x);
                var placeY = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().z);
                var snappedPosition = BuildingGrid.Instance.GetTilePos(gridX, gridY);

                GameObject buildingInstance = Instantiate(_currentBuilding.gameObject,
                    new Vector3(
                        placeX + (width / 2),
                        0f,
                        placeY + (height / 2)),
                    Quaternion.identity);

                isBuildingModeOn = false;
                Destroy(_currentBuildingPreview.gameObject);
                _currentBuildingPreview = null;
                BuildingGrid.Instance.HideTiles();
            }
            else
            {
                Debug.Log("Cannot place building here.");
            }
        }

        public void SelectBuilding(BaseBuilding buildingToBuild)
        {
            if (!_currentBuildingPreview)
            {
                BuildingGrid.Instance.ShowTiles();
                int gridX = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().x);
                int gridY = Mathf.FloorToInt(WorldMouse.Instance.GetPosition().z);
                var snappedPosition = BuildingGrid.Instance.GetTilePos(gridX, gridY);

                _currentBuildingPreview = Instantiate(buildingToBuild.BuildingData.previewPrefab,
                    new Vector3(snappedPosition.x + BUILDING_POS_OFFSET, 0f, snappedPosition.y + BUILDING_POS_OFFSET),
                    Quaternion.identity).transform;
            }
        }
    }
}