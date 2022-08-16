using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GDTV
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            if (!target) return;

            transform.position = target.position;
        }
    }
}
