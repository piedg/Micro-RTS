using OpenUp.Utils;
using TinyRTS.BuildSystem;
using Unity.Mathematics;
using UnityEngine;

public class BuildingGrid : MonoSingleton<BuildingGrid>
{
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float cellSize = 1.0f;

    public int GridWidth => gridWidth;

    public int GridHeight => gridHeight;

    public float CellSize => cellSize;

    private BuildingGridTile[,] _grid;

    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        _grid = new BuildingGridTile[gridWidth, gridHeight];
        for (var x = 0; x < gridWidth; x++)
        {
            for (var y = 0; y < gridHeight; y++)
            {
                _grid[x, y] = new BuildingGridTile(x * cellSize, y * cellSize);
            }
        }
    }

    public float3 GetCellPosition(float2 position)
    {
        if (position.x < 0 || position.x >= gridWidth || position.y < 0 || position.y >= gridHeight)
        {
            Debug.LogError("Grid position out of bounds");
            return float3.zero;
        }

        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);

        return _grid[x, y];
    }

    public float3 GetCellsPosition(float2[,] positions)
    {
        if (positions.GetLength(0) != positions.GetLength(1))
        {
            Debug.LogError("Array positions must be square");
            return float3.zero;
        }

        float3[] positionsArray = new float3[positions.GetLength(0)];
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            positionsArray[i] = GetCellPosition(positions[i, 0]);
        }

        return positionsArray.Length > 0 ? positionsArray[0] : float3.zero;
    }
    
    public bool IsCellOccupied(float2 position)
    {
        if (position.x < 0 || position.x >= gridWidth || position.y < 0 || position.y >= gridHeight)
        {
            Debug.LogError("Grid position out of bounds");
            return false;
        }

        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);

        // Here you would check if the cell is occupied by a building or not.
        // For now, we assume all cells are free.
        return false; // Replace with actual logic to check occupancy
    }
}