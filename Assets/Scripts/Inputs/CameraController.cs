using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

namespace TinyRTS.Inputs
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera virtualCamera;

        [Header("Movement")] [SerializeField] float moveSpeed = 10f;

        [Header("Rotation")] [SerializeField] float rotationSpeed = 10f;

        [Header("Zoom")] [SerializeField] float zoomAmount = 1f;

        [SerializeField] float zoomSpeed = 1f;

        private const float MIN_FOLLOW_Y_OFFSET = 7f;
        private const float MAX_FOLLOW_Y_OFFSET = 14f;

        private CinemachineTransposer _transposer;

        private float3 _targetFollowOffset;

        private void Awake()
        {
            _transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            _targetFollowOffset = _transposer.m_FollowOffset;
        }

        private void LateUpdate()
        {
            Move();
            Rotate();
            Zoom();
        }

        private void Move()
        {
            float2 inputMove = InputManager.Instance.GetCameraMoveVector();
            float3 moveVector = transform.forward * inputMove.y + transform.right * inputMove.x;
            transform.position += (Vector3)moveVector * (moveSpeed * Time.deltaTime);
        }

        private void Rotate()
        {
            float3 rotationVector = Vector3.zero;
            rotationVector.y = InputManager.Instance.GetCameraRotateAmount();
            transform.eulerAngles += (Vector3)rotationVector * (rotationSpeed * Time.deltaTime);
        }

        private void Zoom()
        {
            _targetFollowOffset.y += -InputManager.Instance.GetCameraZoomAmount() * zoomAmount;
            _targetFollowOffset.y = math.clamp(_targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
            _transposer.m_FollowOffset =
                math.lerp(_transposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * zoomSpeed);
        }
    }
}