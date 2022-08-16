using UnityEngine;
using UnityEngine.AI;

namespace GDTV
{
    public class Mover : MonoBehaviour
    {
        private Ray _lastRay;
        private NavMeshAgent _agent;
        private Animator _animator;
        private Camera _camera;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        private void Start()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) MoveToCoordinates();
            UpdateAnimator();
        }

        private void MoveToCoordinates()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit)) _agent.SetDestination(hit.point);
        }

        private void UpdateAnimator()
        {
            var localVelocity = transform.InverseTransformDirection(_agent.velocity);
            _animator.SetFloat(ForwardSpeed, localVelocity.z);
        }
    }
}
