using UnityEngine;

namespace Features.AI.AIStateMachine
{
    public class AIBehaviourTree : MonoBehaviour
    {
        public AITreeComposite StartComposite;
        public AIBlackboard Blackboard;
        [Header("DEBUG")]
        public bool HasCurrentExecutedNode;
        public AITreeComposite CurrentExecutedNode;
        
        public void Tick(float deltaTime)
        {
            StartComposite.TrySetState();
            if (HasCurrentExecutedNode)
            {
                CurrentExecutedNode.OnTick(Time.deltaTime);
            }
        }

        public void SetCurrentNode(AITreeComposite node)
        {
            if (HasCurrentExecutedNode)
            {
                if (node == CurrentExecutedNode)
                {
                    return;
                }
                CurrentExecutedNode.OnExit();
            }

            CurrentExecutedNode = node;
            CurrentExecutedNode.OnEnter();
            HasCurrentExecutedNode = true;
        }
    }
}