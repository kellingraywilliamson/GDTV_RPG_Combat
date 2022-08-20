using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");
        private ActionScheduler _actionScheduler;
        private NavMeshAgent _agent;
        private Animator _animator;
        private Health _health;
        private Ray _lastRay;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            _agent.enabled = _health.IsAlive;
            UpdateAnimator();
        }

        public void Cancel()
        {
            _agent.isStopped = true;
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
    }
}
