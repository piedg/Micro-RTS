using TinyRTS.Patterns;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    public class BuildingGrid : MonoSingleton<BuildingGrid>
    {
        private BuildingGridTile[,] _grid;
        [field: SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }

        [SerializeField] BuildingGridVisualizer gridVisualizer;

        public override void Awake()
        {
            base.Awake();
            gridVisualizer = GetComponent<BuildingGridVisualizer>();
            Initialize(Width, Height);
        }

        private void Initialize(int width, int height)
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

        private bool IsTileOccupied(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return false;

            return _grid[x, y].IsOccupied;
        }

        public BuildingGridTile GetTile(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return null;

            return _grid[x, y];
        }

        public float2 GetTilePos(int x, int y)
        {
            if (IsBoundary(x, y))
            {
                Debug.Log("No Tile found!");
                return float2.zero;
            }

            return _grid[x, y].Position;
        }

        public bool CanPlaceBuilding(int x, int y, int buildingWidth, int buildingHeight)
        {
            if (IsBoundary(x, y) || IsBoundary(x + buildingWidth - 1, y + buildingHeight - 1))
            {
                return false;
            }

            for (int xx = 0; xx < buildingWidth; xx++)
            {
                for (int yy = 0; yy < buildingHeight; yy++)
                {
                    if (IsTileOccupied(x + xx, y + yy))
                        return false;
                }
            }

            return true;
        }

        private bool IsBoundary(int x, int y)
        {
            return x < 0 || x >= Width || y < 0 || y >= Height;
        }

        public void ShowTiles()
        {
            gridVisualizer.ShowTileVisuals();
        }
        
        public void ShowTilesInRange()
        {
            gridVisualizer.ShowTilesInRange();
        }

        public void HideTiles()
        {
            gridVisualizer.HideTileVisuals();
        }
    }
}