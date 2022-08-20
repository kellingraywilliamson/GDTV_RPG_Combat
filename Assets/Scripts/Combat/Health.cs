using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        private static readonly int DieTrigger = Animator.StringToHash("die");
        [SerializeField] private float startingHealth = 100f;
        private Animator _animator;
        private bool _deathAnimationPlayed;

        public float CurrentHealthPoints { get; private set; } = 0;

        public bool IsAlive => CurrentHealthPoints > 0;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            CurrentHealthPoints = startingHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            CurrentHealthPoints = Mathf.Max(CurrentHealthPoints - damageAmount, 0f);
            if (!IsAlive && !_deathAnimationPlayed) Die();
        }

        private void Die()
        {
            _animator.SetTrigger(DieTrigger);
            _deathAnimationPlayed = true;
        }
    }
}
