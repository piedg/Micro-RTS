using System;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    [Serializable]
    public class BuildingGridTile
    {
        private float2 _position;
        private bool _isOccupied;
        
        public float2 Position => _position;
        public bool IsOccupied => _isOccupied;
        public event Action<bool> OnOccupiedChanged;
        
        public BuildingGridTile(float2 position)
        {
            this._position = position;
            _isOccupied = false;
        }
        
        public void SetOccupied(bool occupied)
        {
            _isOccupied = occupied;
            OnOccupiedChanged?.Invoke(_isOccupied);
        }
    }
}