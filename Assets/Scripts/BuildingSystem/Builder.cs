using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private BaseBuilding currentBuilding;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }
        }
        
        private void PlaceBuilding()
        {
            if (currentBuilding)
            {
                Debug.LogWarning("No building selected to place.");
                return;
            }

            // Assuming you have a method to get the position where the building should be placed
            Vector3 placementPosition = GetPlacementPosition();

            // Instantiate the building at the specified position
            Instantiate(currentBuilding, placementPosition, Quaternion.identity);
        }
        private Vector3 GetPlacementPosition()
        {
            // This method should return the position where the building will be placed.
            // For now, we can return a fixed position or use raycasting to find a valid position.
            // Here, we just return the origin for simplicity.
            return Vector3.zero; // Replace with actual logic to determine placement position
        }
        
        private bool TryToPlaceBuilding(float2 position)
        {
            if(BuildingGrid.Instance.GetCellPosition(position) == null)
            {
                Debug.LogWarning("Invalid position for building placement.");
                return false;
            }
            // Implement logic to check if the building can be placed at the given position
            // This could involve checking for collisions, grid alignment, etc.
            // For now, we assume placement is always valid.
            return true; // Replace with actual validation logic
        }
        public void SetCurrentBuilding(BaseBuilding building)
        {
            currentBuilding = building;
        }
        public BaseBuilding GetCurrentBuilding()
        {
            return currentBuilding;
        }
        private void OnDrawGizmos()
        {
            if (currentBuilding != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(GetPlacementPosition(), new Vector3(1, 1, 1)); // Adjust size as needed
            }
        }
        private void OnValidate()
        {
            if (currentBuilding == null)
            {
                Debug.LogWarning("Current building is not set in the Builder component.");
            }
        }
        private void Reset()
        {
            // Automatically set the current building to a default building if none is set
            if (currentBuilding == null)
            {
                currentBuilding = FindObjectOfType<BaseBuilding>();
                if (currentBuilding != null)
                {
                    Debug.Log("Default building set in Builder component.");
                }
                else
                {
                    Debug.LogWarning("No BaseBuilding found in the scene to set as default.");
                }
            }
        }
        
        
    }
    
}