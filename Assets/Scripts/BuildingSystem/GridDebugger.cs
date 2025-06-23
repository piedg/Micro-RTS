using TinyRTS.BuildingSystem;
using UnityEngine;

[ExecuteAlways]
public class GridDebugger : MonoBehaviour
{
    [SerializeField] BuildingGrid buildingGrid;
    private int _gridWidth;
    private int _gridHeight;
    public float cellSize = 1f;
    public bool drawXZ = true;

    private void OnDrawGizmos()
    {
        _gridWidth = buildingGrid.Width;
        _gridHeight = buildingGrid.Height;

        Gizmos.color = Color.green;

        var origin = transform.position;

        if (drawXZ)
        {
            for (var x = 0; x <= _gridWidth; x++)
            {
                var start = origin + new Vector3(x * cellSize, 0, 0);
                var end = start + new Vector3(0, 0, _gridHeight * cellSize);
                Gizmos.DrawLine(start, end);
            }

            for (var z = 0; z <= _gridHeight; z++)
            {
                var start = origin + new Vector3(0, 0, z * cellSize);
                var end = start + new Vector3(_gridWidth * cellSize, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }
        else
        {
            for (var x = 0; x <= _gridWidth; x++)
            {
                var start = origin + new Vector3(x * cellSize, 0, 0);
                var end = start + new Vector3(0, _gridHeight * cellSize, 0);
                Gizmos.DrawLine(start, end);
            }

            for (var y = 0; y <= _gridHeight; y++)
            {
                var start = origin + new Vector3(0, y * cellSize, 0);
                var end = start + new Vector3(_gridWidth * cellSize, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}