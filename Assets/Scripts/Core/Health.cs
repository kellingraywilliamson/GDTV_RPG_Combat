using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        private static readonly int DieTrigger = Animator.StringToHash("die");
        [SerializeField] private float startingHealth = 100f;
        private Animator _animator;
        private bool _deathAnimationPlayed;
        private ActionScheduler _scheduler;

        public float CurrentHealthPoints { get; private set; } = 0;

        public bool IsAlive => CurrentHealthPoints > 0;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _scheduler = GetComponent<ActionScheduler>();
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
            _scheduler.CancelCurrentAction();
        }
    }
}
