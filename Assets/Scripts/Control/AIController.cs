using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float staySuspiciousDuration = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = .5f;
        [SerializeField] private float dwellAtWaypointDuration = 3f;
        private ActionScheduler _actionScheduler;
        private int _currentWaypointIndex;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private Vector3 _originalStartingPosition;
        private GameObject _player;
        private DateTime _timeArrivedAtWaypoint;
        private DateTime _timeLastSawPlayer;

        private double SecondsSinceArrivedAtWaypoint => (DateTime.Now - _timeArrivedAtWaypoint).TotalSeconds;

        private double SecondsSinceLastSawPlayer => (DateTime.Now - _timeLastSawPlayer).TotalSeconds;

        private float DistanceToPlayer => _player == null
            ? Mathf.Infinity
            : Vector3.Distance(transform.position, _player.transform.position);

        private bool PlayerWithinChaseDistance => DistanceToPlayer <= chaseDistance;

        private bool AtWaypoint
        {
            get
            {
                if (patrolPath == null) return false;
                return Vector3.Distance(transform.position, GetCurrentWaypoint()) < waypointTolerance;
            }
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _originalStartingPosition = transform.position;
        }

        private void Update()
        {
            if (!_health.IsAlive) return;

            if (PlayerWithinChaseDistance && _fighter.CanAttack(_player))
                AttackBehaviour();
            else if (SecondsSinceLastSawPlayer <= staySuspiciousDuration)
                SuspicionBehaviour();
            else
                PatroBehaviour();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = PlayerWithinChaseDistance ? Color.red : Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void PatroBehaviour()
        {
            _fighter.Cancel();
            var nextPosition = _originalStartingPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint)
                {
                    _timeArrivedAtWaypoint = DateTime.Now;
                    TargetNextWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (SecondsSinceArrivedAtWaypoint >= dwellAtWaypointDuration) _mover.StartMoveAction(nextPosition);
        }

        private void TargetNextWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypointPosition(_currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            _actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            _timeLastSawPlayer = DateTime.Now;
            _fighter.Attack(_player);
        }
    }
}
