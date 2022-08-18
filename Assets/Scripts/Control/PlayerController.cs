using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) MoveToCursor();
        }

        private void MoveToCursor()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit)) _mover.MoveTo(hit.point);
        }
    }
}
