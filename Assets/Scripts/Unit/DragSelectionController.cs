using UnityEngine;
using TinyRTS.Inputs;
using UnityEngine.InputSystem;

namespace TinyRTS.Unit
{
    public class DragSelectionController : MonoBehaviour
    {
        [SerializeField] private RectTransform selectionBoxUI;
        [SerializeField] private Camera mainCamera;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private bool _isSelecting = false;

        private UnitController _unitSelection;

        private void Start()
        {
            _unitSelection = GetComponent<UnitController>();
            selectionBoxUI.gameObject.SetActive(false);
        }

        private Vector2 GetCanvasMousePosition()
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)selectionBoxUI.parent,
                Input.mousePosition,
                null,
                out mousePos);
            return mousePos;
        }

        void Update()
        {
            if (InputManager.Instance.IsMouseLeftButtonDown())
            {
                _isSelecting = true;
                _startPosition = GetCanvasMousePosition();
                selectionBoxUI.gameObject.SetActive(true);
            }

            if (_isSelecting)
            {
                _endPosition = GetCanvasMousePosition();
                UpdateSelectionBox();

                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    _isSelecting = false;
                    selectionBoxUI.gameObject.SetActive(false);
                    SelectUnits();
                }
            }
        }

        private void UpdateSelectionBox()
        {
            var size = _endPosition - _startPosition;
            var center = _startPosition + (size / 2);

            selectionBoxUI.anchoredPosition = center;
            selectionBoxUI.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
        }

        private void SelectUnits()
        {
            _unitSelection.ClearSelection();

            Vector2 screenStart;
            Vector2 screenEnd;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)selectionBoxUI.parent,
                Input.mousePosition,
                null,
                out screenEnd);

            screenStart = _startPosition;
            screenEnd = _endPosition;

            Canvas canvas = selectionBoxUI.GetComponentInParent<Canvas>();
            RectTransform canvasRect = (RectTransform)canvas.transform;

            var canvasSize = canvasRect.sizeDelta;
            var screenSize = new Vector2(Screen.width, Screen.height);

            screenStart = new Vector2(
                ((screenStart.x + canvasRect.sizeDelta.x / 2) / canvasRect.sizeDelta.x) * screenSize.x,
                ((screenStart.y + canvasRect.sizeDelta.y / 2) / canvasRect.sizeDelta.y) * screenSize.y
            );

            screenEnd = new Vector2(
                ((screenEnd.x + canvasRect.sizeDelta.x / 2) / canvasRect.sizeDelta.x) * screenSize.x,
                ((screenEnd.y + canvasRect.sizeDelta.y / 2) / canvasRect.sizeDelta.y) * screenSize.y
            );

            var min = Vector2.Min(screenStart, screenEnd);
            var max = Vector2.Max(screenStart, screenEnd);

            foreach (var unit in _unitSelection.PlayerUnits)
            {
                var screenPos = mainCamera.WorldToScreenPoint(unit.transform.position);
                if (screenPos.z > 0 &&
                    screenPos.x >= min.x && screenPos.x <= max.x &&
                    screenPos.y >= min.y && screenPos.y <= max.y)
                {
                    _unitSelection.AddToSelection(unit);
                }
            }
        }
    }
}