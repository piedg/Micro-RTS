using Unity.Mathematics;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float cellSize = 1.0f;

    private float3[,] grid;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        grid = new float3[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = new float3(x * cellSize, 0, y * cellSize);
            }
        }
    }

    public Vector3 GetCellPosition(int x, int y)
    {
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
        {
            Debug.LogError("Grid position out of bounds");
            return float3.zero;
        }

        return grid[x, y];
    }
}