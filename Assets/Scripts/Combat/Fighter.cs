using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;

        private Transform _target;
        private Mover _mover;

        private void Start()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (_target == null) return;
            if (!GetIsInRange())
                _mover.MoveTo(_target.position);
            else
                _mover.StopMoving();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
        }

        public void CancelFighting()
        {
            _target = null;
        }
    }
}
