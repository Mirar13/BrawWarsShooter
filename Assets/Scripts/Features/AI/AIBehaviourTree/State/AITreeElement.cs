using System;
using UnityEngine;

namespace Features.AI.AIStateMachine
{
    public abstract class AITreeElement : MonoBehaviour
    {
        public bool IsContinuesElement;
        public bool IsExecuted;

        public bool IsWorking => IsContinuesElement && IsExecuted;

        public virtual bool CanEnterInState()
        {
            return true;
        }

        public virtual bool TryUpdateState()
        {
            if (!CanEnterInState())
            {
                return false;
            }
            
            return true;
        }

        public void OnEnter()
        {
            IsExecuted = true;
            OnEnterInternal();
        }
        public abstract void OnEnterInternal();

        private void Update()
        {
            if (!IsWorking)
            {
                return;
            }

            OnTick(Time.deltaTime);
        }

        public abstract void OnTick(float deltaTime);

        public void OnExit()
        {
            IsExecuted = false;
            OnExitInternal();
        }
        public abstract void OnExitInternal();
    }
}