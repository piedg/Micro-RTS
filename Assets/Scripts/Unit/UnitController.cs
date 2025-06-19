using TinyRTS.Inputs;
using TinyRTS.Unit;
using UnityEngine;
using System.Collections.Generic;
using TinyRTS.Patterns;

namespace TinyRTS.Unit
{
    public class UnitController : MonoSingleton<UnitController>
    {
        [SerializeField]
        private List<BaseUnit> _playerUnits = new List<BaseUnit>();

        public List<BaseUnit> PlayerUnits => _playerUnits;

        private HashSet<BaseUnit> _selectedUnits = new HashSet<BaseUnit>();

        void Update()
        {
            if (InputManager.Instance.IsMouseLeftButtonDown())
            {
                TrySelectUnit();
            }

            if (InputManager.Instance.IsMouseRightButton())
            {
                TryMoveSelectedUnits();
            }
        }

        private void TrySelectUnit()
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out BaseUnit unit))
                {
                    if (unit)
                    {
                        ClearSelection();
                        _selectedUnits.Add(unit);
                        unit.Select();
                    }
                    else
                    {
                        ClearSelection();
                    }
                }
            }
        }

        private void TryMoveSelectedUnits()
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int unitsPerRow = Mathf.CeilToInt(Mathf.Sqrt(_selectedUnits.Count));
                float spacing = 2.0f;
                int index = 0;

                foreach (BaseUnit unit in _selectedUnits)
                {
                    int row = index / unitsPerRow;
                    int col = index % unitsPerRow;

                    Vector3 offset = new Vector3(col * spacing, 0, row * spacing);
                    Vector3 targetPosition = hit.point + offset;

                    unit.MoveTo(targetPosition);
                    index++;
                }
            }
        }

        public void AddToSelection(BaseUnit unit)
        {
            if (unit != null && !_selectedUnits.Contains(unit))
            {
                _selectedUnits.Add(unit);
                unit.Select();
            }
        }

        public void RemoveFromSelection(BaseUnit unit)
        {
            if (unit != null && _selectedUnits.Contains(unit))
            {
                _selectedUnits.Remove(unit);
                unit.Deselect();
            }
        }

        public void ClearSelection()
        {
            foreach (var unit in _selectedUnits)
            {
                unit.Deselect();
            }
            _selectedUnits.Clear();
        }

        public bool IsSelected(BaseUnit unit)
        {
            return _selectedUnits.Contains(unit);
        }
    }
}