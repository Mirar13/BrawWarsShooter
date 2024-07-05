using UnityEngine;

namespace Features.AI.AIStateMachine.Composite
{
    public class AISequenceTreeComposite : AITreeComposite
    {
        public AITreeTask[] ElementsForExecute;

        [Header("DEBUG")]
        public int CurrentExecutedIndex;
        public bool HasCurrentExecutedNode;
        public AITreeTask CurrentExecutedNode;

       public override bool CanEnterInComposite()
        {
            return !IsContinuesElement || !IsExecuted;
        }

       public override void TrySetState()
       {
           behaviourTree.SetCurrentNode(this);
           IsExecuted = true;
       }

       public override void OnEnter()
        {
            if (ElementsForExecute.Length <= 0)
            {
                IsExecuted = false;
                return;
            }
            
            CurrentExecutedIndex = -1;
            if (!TryExecuteNextElement())
            {
                IsExecuted = false;
            }
        }
        
        public override void OnExit()
        {
            IsExecuted = false;
            CurrentExecutedIndex = -1;
            CurrentExecutedNode = null;
            HasCurrentExecutedNode = false;
        }

        public override void OnTickInternal(float deltaTime)
        {
            if (HasCurrentExecutedNode && CurrentExecutedNode.IsWorking)
            {
                return;
            }

            if (!TryExecuteNextElement())
            {
                OnExit();
            }
        }
        
        private bool TryExecuteNextElement()
        {
            CurrentExecutedIndex++;
            if (CurrentExecutedIndex >= ElementsForExecute.Length)
            {
                IsExecuted = false;
                return false;
            }

            if (HasCurrentExecutedNode)
            {
                CurrentExecutedNode.OnExit();
            }

            CurrentExecutedNode = ElementsForExecute[CurrentExecutedIndex];
            CurrentExecutedNode.OnEnter();
            HasCurrentExecutedNode = true;
            return true;
        }
    }
}