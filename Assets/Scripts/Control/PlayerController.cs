using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _camera;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;

        private void Start()
        {
            _camera = Camera.main;
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (!_health.IsAlive) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            foreach (var hit in Physics.RaycastAll(GetMouseRay()))
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!_fighter.CanAttack(target.gameObject)) continue;
                if (Input.GetMouseButtonDown(0)) _fighter.Attack(target.gameObject);
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
