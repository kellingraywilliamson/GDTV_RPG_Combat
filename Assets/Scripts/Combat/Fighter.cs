using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private static readonly int AttackTrigger = Animator.StringToHash("attack");
        private static readonly int AbortAttackTrigger = Animator.StringToHash("abortAttack");
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private DateTime _lastAttackTime;

        private Mover _mover;
        private Health _target;

        private bool InWeaponRange => Vector3.Distance(transform.position, _target.transform.position) < weaponRange;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_target == null) return;
            if (!_target.IsAlive) return;

            if (!InWeaponRange)
            {
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        public void Cancel()
        {
            _animator.SetTrigger(AbortAttackTrigger);
            _target = null;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if (!((DateTime.Now - _lastAttackTime).TotalSeconds >= timeBetweenAttacks)) return;
            _animator.SetTrigger(AttackTrigger);
            _lastAttackTime = DateTime.Now;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        private void Hit()
        {
            if (_target == null) return;
            _target.TakeDamage(weaponDamage);
        }
    }
}
