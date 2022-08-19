using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private MonoBehaviour _currentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (action == _currentAction) return;

            if (_currentAction != null) Debug.Log($"Cancelling {_currentAction}...");

            _currentAction = action;
        }
    }
}
