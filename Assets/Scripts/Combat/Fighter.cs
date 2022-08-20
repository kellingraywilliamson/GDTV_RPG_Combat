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
        private bool TimeBetweenAttacksElapsed => (DateTime.Now - _lastAttackTime).TotalSeconds >= timeBetweenAttacks;

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
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            _animator.ResetTrigger(AttackTrigger);
            _animator.SetTrigger(AbortAttackTrigger);
        }

        public bool CanAttack(CombatTarget target)
        {
            return target != null && target.GetComponent<Health>().IsAlive;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if (!TimeBetweenAttacksElapsed) return;
            TriggerAttack();
            _lastAttackTime = DateTime.Now;
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger(AbortAttackTrigger);
            _animator.SetTrigger(AttackTrigger);
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
