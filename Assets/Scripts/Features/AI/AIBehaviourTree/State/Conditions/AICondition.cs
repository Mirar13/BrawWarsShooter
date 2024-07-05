using UnityEngine;

namespace Features.AI.AIStateMachine.Conditions
{
    public abstract class AICondition : MonoBehaviour
    {
        public AITreeComposite Composite;
        public abstract bool IsSuccess();
    }
}