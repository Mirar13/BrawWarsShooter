using UnityEngine;

namespace Features.AI.AIStateMachine.Task
{
    public class AIWaitTask : AITreeTask
    {
        public float TimeForWait;
        private float _currentTime;

        private protected override void OnEnterInternal()
        {
            Debug.Log("Wait Task EnterInternal");
            _currentTime = 0f;
        }
        
        private protected override void OnExitInternal()
        {
            Debug.Log("Wait Task ExitInternal");
            _currentTime = 0f;
        }

        private protected override void Tick(float deltaTime)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= TimeForWait)
            {
                IsExecuted = false;
            }
        }
    }
}