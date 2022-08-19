using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private Ray _lastRay;
        private NavMeshAgent _agent;
        private Animator _animator;
        private Fighter _fighter;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _fighter = GetComponent<Fighter>();
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

        public void StartMoveAction(Vector3 destination)
        {
            _fighter.CancelFighting();
            MoveTo(destination);
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
