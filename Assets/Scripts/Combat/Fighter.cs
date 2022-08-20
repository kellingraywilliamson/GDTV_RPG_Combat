using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private float timeBetweenAttacks = 1f;

        private Transform _target;
        private Mover _mover;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private DateTime _lastAttackTime;

        private static readonly int AttackTrigger = Animator.StringToHash("attack");

        private bool InWeaponRange => Vector3.Distance(transform.position, _target.position) < weaponRange;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_target == null) return;
            if (!InWeaponRange)
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (!((DateTime.Now - _lastAttackTime).TotalSeconds >= timeBetweenAttacks)) return;
            _animator.SetTrigger(AttackTrigger);
            _lastAttackTime = DateTime.Now;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }

        private void Hit()
        {
            if (_target == null) return;
            var targetHealth = _target.GetComponent<Health>();
            if (targetHealth != null) targetHealth.TakeDamage(weaponDamage);
        }
    }
}
