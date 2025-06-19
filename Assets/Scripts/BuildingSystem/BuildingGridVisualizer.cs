using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    public class BuildingGridVisualizer : MonoBehaviour
    {
        [SerializeField] private BuildingGridTileVisual tilePrefab;
        [SerializeField] List<BuildingGridTileVisual> tileVisuals;

        private void Start()
        {
            for (int i = 0; i < BuildingGrid.Instance.Width; i++)
            {
                for (int j = 0; j < BuildingGrid.Instance.Height; j++)
                {
                    var tilePosition = new float2(i, j);
                    var tileVisual = Instantiate(tilePrefab,
                        new Vector3(tilePosition.x, 1f, tilePosition.y), Quaternion.Euler(90, 0, 0));
                    tileVisual.GetComponent<BuildingGridTileVisual>().Initialize(tilePosition);
                    tileVisuals.Add(tileVisual);
                }
            }
            
            HideTileVisuals();
        }

        public void ShowTileVisuals()
        {
            foreach (var tileVisual in tileVisuals)
            {
                tileVisual.gameObject.SetActive(true);
            }
        }

        public void HideTileVisuals()
        {
            foreach (var tileVisual in tileVisuals)
            {
                tileVisual.gameObject.SetActive(false);
            }
        }
    }
}