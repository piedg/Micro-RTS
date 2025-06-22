using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TinyRTS.Unit
{
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] private UnitSO unitData;

        public UnitSO UnitData => unitData;

        private NavMeshAgent _agent;
        private Image _selectorImage;
        private float _acceptanceRadius = 0.5f;

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
            if (math.distancesq(transform.position, targetPosition) < _acceptanceRadius * _acceptanceRadius)
            {
                return;
            }

            _agent.SetDestination(targetPosition);
        }

        public virtual void CancelAction()
        {
        }
    }
}