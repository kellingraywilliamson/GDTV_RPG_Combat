using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;

        private Transform _target;
        private Mover _mover;
        private ActionScheduler _actionScheduler;

        private bool InWeaponRange => Vector3.Distance(transform.position, _target.position) < weaponRange;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (_target == null) return;
            if (!InWeaponRange)
                _mover.MoveTo(_target.position);
            else
                _mover.Cancel();
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
    }
}
