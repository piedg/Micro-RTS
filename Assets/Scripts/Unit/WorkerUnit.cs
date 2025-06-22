using System;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.Unit
{
    public class WorkerUnit : BaseUnit
    {
        [SerializeField] private float gatherRange = 2f;
        [SerializeField] private float gatherInterval = 2f;
        private float _gatherTimer = 0f;
        private BaseResource _currentGatherable;

        private void Update()
        {
            if (!_currentGatherable)
            {
                return;
            }

            var distSqr = math.distancesq(transform.position, _currentGatherable.transform.position);
            if (distSqr <= gatherRange * gatherRange)
            {
                if(_currentGatherable.IsDepleted)
                {
                    StopGathering();
                    return;
                }
                
                _gatherTimer += Time.deltaTime;
                if (_gatherTimer >= gatherInterval)
                {
                    _currentGatherable.Gather();
                    _gatherTimer = 0f;
                }
            }
            else
            {
                MoveTo(_currentGatherable.transform.position);
            }
        }

        public void StartGathering(BaseResource gatherableResource)
        {
            if (!gatherableResource)
            {
                return;
            }
            
            _currentGatherable = gatherableResource;
            _gatherTimer = 0f;

            var distSqr = math.distancesq(transform.position, gatherableResource.transform.position);
            if (distSqr > gatherRange * gatherRange)
            {
                MoveTo(gatherableResource.transform.position);
            }
        }

        private void StopGathering()
        {
            _currentGatherable = null;
            _gatherTimer = 0f;
        }

        public override void CancelAction()
        {
            StopGathering();
        }
    }
}