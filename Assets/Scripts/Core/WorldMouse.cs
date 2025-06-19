using OpenUp.Utils;
using TinyRTS.Inputs;
using Unity.Mathematics;
using UnityEngine;


namespace TinyRTS.Core 
{
    public class WorldMouse : MonoSingleton<WorldMouse>
    {
        [SerializeField] private LayerMask mousePlaneLayer;

        void Update()
        {
            transform.position = GetPosition();
        }

        public float3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, Instance.mousePlaneLayer);

            return hit.point;
        }
    }
}