#define USE_NEW_INPUT_SYSTEM
using TinyRTS.Patterns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TinyRTS.Inputs
{
    public class InputManager : MonoSingleton<InputManager>
    {
        private PlayerInputs _playerInputActions;

        public override void Awake()
        {
            base.Awake();

            _playerInputActions = new PlayerInputs();
            _playerInputActions.Player.Enable();
        }

        public Vector2 GetMouseScreenPosition()
        {
#if USE_NEW_INPUT_SYSTEM
            return Mouse.current.position.ReadValue();
#else
            return Input.mousePosition;
#endif
        }

        public float GetMouseDeltaValue()
        {
            return Mouse.current.delta.y.ReadValue();
        }

        public bool IsMouseLeftButtonDown()
        {
#if USE_NEW_INPUT_SYSTEM
            return _playerInputActions.Player.MouseLeftButton.WasPressedThisFrame();
#else
            return Input.GetMouseButtonDown(0);
#endif
        }

        public bool IsMouseRightButton()
        {
#if USE_NEW_INPUT_SYSTEM
            return _playerInputActions.Player.MouseRightPressed.IsPressed();
#else
            return Input.GetMouseButton(1);
#endif
        }

        public Vector2 GetCameraMoveVector()
        {
#if USE_NEW_INPUT_SYSTEM
            return _playerInputActions.Player.CameraMovement.ReadValue<Vector2>();
#else
   Vector2 inputMove = Vector2.zero;


            if (Input.GetKey(KeyCode.W))
            {
                inputMove.y = +1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputMove.x = -1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputMove.y = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputMove.x = +1f;
            }

            return inputMove;
#endif
        }


        public float GetCameraRotateAmount()
        {
#if USE_NEW_INPUT_SYSTEM

            return IsMouseRightButton() ? _playerInputActions.Player.CameraRotate.ReadValue<float>() : 0;
#else
            if (IsMouseRightButton())
            {
                if (GetMouseDeltaValue() < 0)
                {
                    return -1f;
                }
                if (GetMouseDeltaValue() > 0)
                {
                    return +1f;
                }
            }

            return 0;
#endif
        }

        public float GetCameraZoomAmount()
        {
#if USE_NEW_INPUT_SYSTEM
            return _playerInputActions.Player.CameraZoom.ReadValue<float>();
#else
            if (Input.mouseScrollDelta.y > 0)
            {
                return -1f;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                return +1f;
            }

            return 0;
#endif
        }
    }
}