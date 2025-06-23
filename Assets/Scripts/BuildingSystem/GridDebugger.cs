using UnityEngine;

[ExecuteAlways]  // Per disegnarla anche in editor
public class GridDebugger : MonoBehaviour
{
    public int width = 10;         // Numero di celle in X
    public int height = 10;        // Numero di celle in Z (o Y)
    public float cellSize = 1f;    // Dimensione di ogni cella
    public bool drawXZ = true;     // Piano su cui disegnare la griglia (XZ o XY)

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 origin = transform.position;

        if (drawXZ)
        {
            // Disegna su piano XZ
            for (int x = 0; x <= width; x++)
            {
                Vector3 start = origin + new Vector3(x * cellSize, 0, 0);
                Vector3 end = start + new Vector3(0, 0, height * cellSize);
                Gizmos.DrawLine(start, end);
            }

            for (int z = 0; z <= height; z++)
            {
                Vector3 start = origin + new Vector3(0, 0, z * cellSize);
                Vector3 end = start + new Vector3(width * cellSize, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }
        else
        {
            // Disegna su piano XY (ad esempio)
            for (int x = 0; x <= width; x++)
            {
                Vector3 start = origin + new Vector3(x * cellSize, 0, 0);
                Vector3 end = start + new Vector3(0, height * cellSize, 0);
                Gizmos.DrawLine(start, end);
            }

            for (int y = 0; y <= height; y++)
            {
                Vector3 start = origin + new Vector3(0, y * cellSize, 0);
                Vector3 end = start + new Vector3(width * cellSize, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}