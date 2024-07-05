using UnityEngine;

namespace Features.AI.AIStateMachine.Task
{
    public class AILogTask : AITreeTask
    {
        public string LogStr;

        private protected override void OnEnterInternal()
        {
            Debug.Log(LogStr);
        }
        
        private protected override void OnExitInternal()
        {
            
        }

        private protected override void Tick(float deltaTime)
        {
            
        }
    }
}