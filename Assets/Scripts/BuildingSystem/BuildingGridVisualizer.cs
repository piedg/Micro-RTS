using System;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class BuildingGridVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject gridCellPrefab;
        [SerializeField] BuildingGrid buildingGrid;

        private void Start()
        {
            VisualizeGrid();
        }

        private void VisualizeGrid()
        {
            for (int x = 0; x < buildingGrid.GridWidth; x++)
            {
                for (int y = 0; y < buildingGrid.GridHeight; y++)
                {
                    float3 cellPosition = buildingGrid.GetCellPosition(new float2(x, y));
                    var yOffset = new float3(0, 0.02f, 0);
                    GameObject cell = Instantiate(gridCellPrefab, cellPosition + yOffset, Quaternion.Euler(90, 0, 0));
                    //cell.transform.localScale = new Vector3(_buildingGrid.CellSize, 1, _buildingGrid.CellSize);
                    cell.name = $"Cell_{x}_{y}";
                }
            }
        }
    }
}