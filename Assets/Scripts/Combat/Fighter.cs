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

            var isInRange = Vector3.Distance(transform.position, _target.position) < weaponRange;
            if (!isInRange)
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.StopMoving();
                _target = null;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
        }
    }
}
