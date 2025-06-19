using OpenUp.Utils;
using TinyRTS.BuildSystem;
using Unity.Mathematics;
using UnityEngine;

public class BuildingGrid : MonoSingleton<BuildingGrid>
{
    private BuildingGridTile[,] _grid;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public override void Awake()
    {
        base.Awake();
        Initialize(10, 10);
    }

    public void Initialize(int width, int height)
    {
        Width = width;
        Height = height;
        _grid = new BuildingGridTile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _grid[x, y] = new BuildingGridTile(new float2(x, y));
            }
        }
    }

    public bool IsTileOccupied(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return false;

        return _grid[x, y].IsOccupied;
    }

    public void SetTileOccupied(int x, int y, bool occupied)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return;

        _grid[x, y].SetOccupied(occupied);
    }

    public BuildingGridTile GetTile(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return null;

        return _grid[x, y];
    }

    public bool CanPlaceBuilding(int x, int y, int buildingWidth, int buildingHeight)
    {
        if (IsBoundary(x, y) || IsBoundary(x + buildingWidth - 1, y + buildingHeight - 1))
        {
            return false;
        }

        for (int i = 0; i < buildingWidth; i++)
        {
            for (int j = 0; j < buildingHeight; j++)
            {
                if (IsTileOccupied(x + i, y + j))
                    return false;
            }
        }

        return true;
    }

    private bool IsBoundary(int x, int y)
    {
        return x < 0 || x >= Width || y < 0 || y >= Height;
    }

    public void UpdateGrid()
    {
        for (int i = 0; i < BuildingGrid.Instance.Width; i++)
        {
            for (int j = 0; j < BuildingGrid.Instance.Height; j++)
            {
                var tile = BuildingGrid.Instance.GetTile(i, j);
                if (tile != null)
                {
                    var tileVisual = FindObjectOfType<BuildingGridTileVisual>();
                    if (tileVisual != null)
                    {
                        tileVisual.UpdateVisual();
                    }
                }
            }
        }
    }
}