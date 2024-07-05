using UnityEngine;

namespace Features.AI.AIStateMachine
{
    public abstract class AITreeComposite : MonoBehaviour
    {
        [SerializeField] protected internal AIBehaviourTree behaviourTree;
        public bool IsContinuesElement;
        public bool IsExecuted;

        public abstract bool CanEnterInComposite();

        public abstract void TrySetState();
        public abstract void OnEnter();
        public abstract void OnExit();

        public void OnTick(float deltaTime)
        {
            if (!IsContinuesElement || !IsExecuted)
            {
                return;
            }
            OnTickInternal(deltaTime);
        }

        public virtual void OnTickInternal(float deltaTime)
        {
            
        }
    }
}