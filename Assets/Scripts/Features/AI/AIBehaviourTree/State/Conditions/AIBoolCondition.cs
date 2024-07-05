using UnityEngine;

namespace Features.AI.AIStateMachine.Conditions
{
    public class AIBoolCondition : AICondition
    {
        public string BlackboardKey;
        public bool IsValue;
        
        public override bool IsSuccess()
        {
            var blackboard = Composite.behaviourTree.Blackboard;
            if (blackboard is IAIBlackboardBool blackboardBool)
            {
                return blackboardBool.GetBool(BlackboardKey) == IsValue;
            }
            return false;
        }
    }
}