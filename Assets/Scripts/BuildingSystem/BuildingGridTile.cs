using System;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    [Serializable]
    public class BuildingGridTile
    {
        private float2 position;
        private bool isOccupied;
        
        public float2 Position => position;
        public bool IsOccupied => isOccupied;
        public event Action<bool> OnOccupiedChanged;
        
        public BuildingGridTile(float2 position)
        {
            this.position = position;
            isOccupied = false;
        }
        
        public void SetOccupied(bool occupied)
        {
            isOccupied = occupied;
            OnOccupiedChanged?.Invoke(isOccupied);
        }
    }
}