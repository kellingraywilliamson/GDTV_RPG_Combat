using UnityEngine;
using UnityEngine.AI;

namespace GDTV
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Ray _lastRay;
        private NavMeshAgent _agent;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) MoveToCoordinates();
        }

        private void MoveToCoordinates()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit)) _agent.SetDestination(hit.point);
        }
    }
}
