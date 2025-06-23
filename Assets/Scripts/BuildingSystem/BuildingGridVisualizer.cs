using System.Collections.Generic;
using TinyRTS.Core;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    public class BuildingGridVisualizer : MonoBehaviour
    {
        [SerializeField] private BuildingGridTileVisual tilePrefab;
        [SerializeField] List<BuildingGridTileVisual> tileVisuals;

        [SerializeField] private List<GameObject> envObjects;
        [SerializeField] LayerMask layerMask;
        private readonly int _activeTilesRange = 15;

        private void Start()
        {
            for (var x = 0; x < BuildingGrid.Instance.Width; x++)
            {
                for (var y = 0; y < BuildingGrid.Instance.Height; y++)
                {
                    var tilePosition = new float2(x, y);
                    var tileVisual = Instantiate(tilePrefab,
                        new Vector3(tilePosition.x, 1f, tilePosition.y), Quaternion.Euler(90, 0, 0));

                    if (tileVisual.TryGetComponent(out BuildingGridTileVisual buildingGridTileVisual))
                    {
                        buildingGridTileVisual.Initialize(tilePosition);
                        tileVisuals.Add(tileVisual);
                    }
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

        public void ShowTilesInRange()
        {
            float3 mouseWorldPos = WorldMouse.Instance.GetPosition();

            var mouseTileX = math.round(mouseWorldPos.x);
            var mouseTileY = math.round(mouseWorldPos.z);

            foreach (var tileVisual in tileVisuals)
            {
                float2 tilePos = tileVisual.Tile.Position;

                var dist = math.distancesq(new float2(mouseTileX, mouseTileY), tilePos);

                tileVisual.gameObject.SetActive(dist <= _activeTilesRange * _activeTilesRange);
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