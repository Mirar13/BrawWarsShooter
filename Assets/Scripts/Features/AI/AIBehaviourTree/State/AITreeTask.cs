using System;
using UnityEngine;

namespace Features.AI.AIStateMachine
{
    public abstract class AITreeTask : MonoBehaviour
    {
        public bool IsContinuesElement;
        public bool IsExecuted;

        public bool IsWorking => IsContinuesElement && IsExecuted;

        public void OnEnter()
        {
            IsExecuted = true;
            OnEnterInternal();
        }

        private protected abstract void OnEnterInternal();
        
        public void OnExit()
        {
            IsExecuted = false;
            OnExitInternal();
        }

        private protected abstract void OnExitInternal();

        public void Update()
        {
            if (!IsWorking)
            {
                return;
            }
            Tick(Time.deltaTime);
        }

        private protected abstract void Tick(float deltaTime);
    }
}