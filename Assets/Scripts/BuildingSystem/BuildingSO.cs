using UnityEngine;

namespace TinyRTS.BuildSystem
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Building System/Building")]
    public class BuildingSO : ScriptableObject
    {
        public GameObject prefab;
        public string buildingName;
        public int width = 1;
        public int height = 1;
        public int cost = 100;
        public int buildTime = 5;
        public string description;
        public Sprite icon;
    }
}