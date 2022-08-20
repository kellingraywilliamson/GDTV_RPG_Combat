using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private Fighter _fighter;
        private Health _health;
        private GameObject _player;
        private float DistanceToPlayer => Vector3.Distance(transform.position, _player.transform.position);
        private bool PlayerWithinChaseDistance => DistanceToPlayer <= chaseDistance;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (!_health.IsAlive) return;

            if (PlayerWithinChaseDistance && _fighter.CanAttack(_player))
                _fighter.Attack(_player);
            else
                _fighter.Cancel();
        }
    }
}
