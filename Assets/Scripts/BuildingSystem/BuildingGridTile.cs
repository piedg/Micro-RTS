using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class BuildingGridTile
    {
        [SerializeField] private float2 position;
        public float2 Position => position;
        public bool isOccupied;
        
        public BuildingGridTile(float x, float y)
        {
            position = new float2(x, y);
            isOccupied = false;
        }
    }
}