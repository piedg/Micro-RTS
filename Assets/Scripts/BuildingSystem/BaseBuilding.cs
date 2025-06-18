using UnityEngine;

namespace TinyRTS.BuildSystem
{
    public class BaseBuilding : MonoBehaviour
    {
        [SerializeField] BuildingSO buildingData;
        public BuildingSO BuildingData => buildingData;
        
    }
}