using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float waypointGizmoRadius = 0.3f;
        private int ChildCount => transform.childCount;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;

            for (var i = 0; i < ChildCount; i++)
            {
                Gizmos.DrawSphere(GetWaypointPosition(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(GetNextIndex(i)));
            }
        }

        public int GetNextIndex(int i)
        {
            return i + 1 < ChildCount ? i + 1 : 0;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
