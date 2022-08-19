using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private Ray _lastRay;
        private NavMeshAgent _agent;
        private Animator _animator;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateAnimator();
        }


        public void MoveTo(Vector3 destination)
        {
            _agent.SetDestination(destination);
            _agent.isStopped = false;
        }

        public void StopMoving()
        {
            _agent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            var localVelocity = transform.InverseTransformDirection(_agent.velocity);
            _animator.SetFloat(ForwardSpeed, localVelocity.z);
        }
    }
}
