using System.Linq;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _camera;
        private Fighter _fighter;
        private Mover _mover;

        private void Start()
        {
            _camera = Camera.main;
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            foreach (var hit in Physics.RaycastAll(GetMouseRay()))
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (!_fighter.CanAttack(target)) continue;
                if (Input.GetMouseButtonDown(0)) _fighter.Attack(target);
                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            if (!Physics.Raycast(GetMouseRay(), out var hit)) return false;
            if (Input.GetMouseButtonDown(0)) _mover.StartMoveAction(hit.point);
            return true;
        }

        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
