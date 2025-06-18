using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class BuildingGridVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject gridCellPrefab;

        BuildingGrid _buildingGrid;
        private void Awake()
        {
            _buildingGrid = GetComponent<BuildingGrid>();
        }
    }
}