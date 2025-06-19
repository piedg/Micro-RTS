using UnityEngine;

namespace TinyRTS.BuildingSystem
{
    public class BaseBuilding : MonoBehaviour
    {
        [SerializeField] private BuildingSO buildingData;
        public BuildingSO BuildingData => buildingData;
        
    }
}