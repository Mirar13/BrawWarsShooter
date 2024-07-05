using UnityEngine;
using UnityEngine.AI;

namespace Features.AI.AIStateMachine.Task
{
    public class AIGetPointInRadiusTask : AITreeTask
    {
        public AIBlackboard Blackboard;
        public string TargetPositionKey;
        public string Key;
        public float Radius;
        
        private protected override void OnEnterInternal()
        {
            if (Blackboard is IAIBlackboardVector3 blackboardVector3)
            {
                var targetPosition = blackboardVector3.GetVector(TargetPositionKey);
                var randomPoint = Random.insideUnitCircle * Radius;
                var targetPoint = targetPosition + new Vector3(randomPoint.x, 0, randomPoint.y);
                NavMesh.SamplePosition(targetPoint, out var hit, 1000f, NavMesh.AllAreas);
                blackboardVector3.SetVector(Key, hit.position);
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