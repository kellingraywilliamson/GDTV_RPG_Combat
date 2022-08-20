using System;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private GameObject _player;
        private Fighter _fighter;
        private float DistanceToPlayer => Vector3.Distance(transform.position, _player.transform.position);
        private bool PlayerWithinChaseDistance => DistanceToPlayer <= chaseDistance;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (PlayerWithinChaseDistance && _fighter.CanAttack(_player))
                _fighter.Attack(_player);
            else
                _fighter.Cancel();
        }
    }
}
