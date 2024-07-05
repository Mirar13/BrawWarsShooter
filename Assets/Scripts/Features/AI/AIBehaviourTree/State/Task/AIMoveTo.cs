using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Features.AI.AIStateMachine.Task
{
    public class AIMoveTo : AITreeTask
    {
        public AIBlackboard Blackboard;
        public string Key;
        public float CompleteDistanceToPoint = 1f;

        private Vector3 _targetPosition;
        
        private protected override void OnEnterInternal()
        {
            if (Blackboard is IAIBlackboardVector3 blackboardVector3)
            {
                _targetPosition = blackboardVector3.GetVector(Key);
                Blackboard.Agent.isStopped = false;
                Blackboard.Agent.SetDestination(_targetPosition);
            }
        }

        private protected override void OnExitInternal()
        {
            _targetPosition = Vector3.zero;
        }

        private protected override void Tick(float deltaTime)
        {
            if (Vector3.Distance(transform.position, _targetPosition) <= CompleteDistanceToPoint)
            {
                IsExecuted = false;
                Blackboard.Agent.isStopped = true;
            }
        }
    }
}