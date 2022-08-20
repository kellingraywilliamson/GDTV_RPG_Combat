using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float waypointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;

            for (var i = 0; i < transform.childCount; i++)
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointGizmoRadius);
        }
    }
}
