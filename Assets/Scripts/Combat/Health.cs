using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void TakeDamage(float damageAmount)
        {
            health = Mathf.Max(health - damageAmount, 0f);
            Debug.Log(health);
        }
    }
}
