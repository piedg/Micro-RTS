using UnityEngine;

namespace TinyRTS.Unit
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Gathering Resources/Create Resource", order = 2)]
    public class ResourceSO : ScriptableObject
    {
        public enum ResourceType
        {
            Wood,
            Stone,
            Gold,
        }

        public int width = 1;
        public int height = 1;
        public string resourceName;
        public int startingValue = 100;
        public ResourceType type;
        public Sprite icon;
        public GameObject prefab;
        public string description;
    }
}