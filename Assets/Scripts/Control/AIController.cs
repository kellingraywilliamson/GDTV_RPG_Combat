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
        private ActionScheduler _actionScheduler;
        private Fighter _fighter;
        private Health _health;

        private Mover _mover;
        private Vector3 _originalStartingPosition;
        private GameObject _player;
        private DateTime _timeLastSawPlayer;
        private double SecondsSinceLastSawPlayer => (DateTime.Now - _timeLastSawPlayer).TotalSeconds;

        private float DistanceToPlayer => _player == null
            ? Mathf.Infinity
            : Vector3.Distance(transform.position, _player.transform.position);

        private bool PlayerWithinChaseDistance => DistanceToPlayer <= chaseDistance;

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
                GuardBehaviour();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = PlayerWithinChaseDistance ? Color.red : Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void GuardBehaviour()
        {
            _fighter.Cancel();
            _mover.StartMoveAction(_originalStartingPosition);
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
