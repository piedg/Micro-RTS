using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TinyRTS.Unit
{
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] private UnitSO unitData;
        private NavMeshAgent _agent;
        private Image _selectorImage;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _selectorImage = GetComponentInChildren<Image>();
            _selectorImage.enabled = false;
        }

        public void Select()
        {
            _selectorImage.enabled = true;
        }

        public void Deselect()
        {
            _selectorImage.enabled = false;
        }

        public void MoveTo(Vector3 targetPosition)
        {
            _agent.SetDestination(targetPosition);
        }
    }
}