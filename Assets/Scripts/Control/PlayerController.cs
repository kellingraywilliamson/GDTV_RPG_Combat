using System.Linq;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Camera _camera;
        private Fighter _fighter;

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
            Debug.Log("At the end of the world!");
        }

        private bool InteractWithCombat()
        {
            foreach (var hit in Physics.RaycastAll(GetMouseRay())
                         .Where(x => x.transform.GetComponent<CombatTarget>() != null))
            {
                var target = hit.transform.GetComponent<CombatTarget>();

                if (Input.GetMouseButtonDown(0)) _fighter.Attack(target);
                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            if (!Physics.Raycast(GetMouseRay(), out var hit)) return false;
            if (Input.GetMouseButtonDown(0)) _mover.MoveTo(hit.point);
            return true;
        }

        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
