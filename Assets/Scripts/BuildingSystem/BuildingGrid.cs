using System;
using TinyRTS.Core;
using TinyRTS.Patterns;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class BuildingGrid : MonoSingleton<BuildingGrid>
    {
        private BuildingGridTile[,] _grid;
        [field: SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }

        public override void Awake()
        {
            base.Awake();
            Initialize(Width, Height);
        }

        private void Update()
        {
            //float3 mousePosition = WorldMouse.Instance.GetPosition();
            //Debug.Log($"Current Tile {GetTilePos((int)mousePosition.x, (int)mousePosition.z)}");
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
    }
}