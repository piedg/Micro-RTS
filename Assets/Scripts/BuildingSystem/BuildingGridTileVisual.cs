using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class BuildingGridTileVisual : MonoBehaviour
    {
        BuildingGridTile tile;

        public void Initialize(float2 position)
        {
            tile = new BuildingGridTile(position);
            transform.position = new Vector3(position.x, 0.09f, position.y);
            UpdateVisual();
        }

        public void UpdateVisual()
        {
            if (tile.IsOccupied)
            {
                // Optionally, change color or appearance to indicate occupation
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                // Reset color or appearance for unoccupied state
                GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
}