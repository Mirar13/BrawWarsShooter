using Features.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Features.AI.AIStateMachine.Task
{
    public class AIGetPointTask : AITreeTask
    {
        public AIBlackboard Blackboard;
        public string Key;
        public PointType PointType;
        
        private protected override void OnEnterInternal()
        {
            if (Blackboard is IAIBlackboardVector3 blackboardVector3)
            {
                blackboardVector3.SetVector(Key, AINavigationMap.Instance.GetPoint(PointType));
            }
        }

        private protected override void OnExitInternal()
        {
            
        }

        private protected override void Tick(float deltaTime)
        {
            
        }
    }
}