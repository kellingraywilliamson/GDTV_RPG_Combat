using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private Ray _lastRay;
        private NavMeshAgent _agent;
        private Animator _animator;
        private ActionScheduler _actionScheduler;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
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
            _actionScheduler.StartAction(this);
            MoveTo(destination);
        }


        private void UpdateAnimator()
        {
            var localVelocity = transform.InverseTransformDirection(_agent.velocity);
            _animator.SetFloat(ForwardSpeed, localVelocity.z);
        }

        public void Cancel()
        {
            _agent.isStopped = true;
        }
    }
}
