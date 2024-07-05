using System;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Executions
{
    public class GamePlayUpdateExecution : GamePlayExecution
    {
        public bool IsStarted = false;
        public float TimeToCallExecution;
        public GamePlayExecution[] EveryTickExecution;
        public GamePlayExecution[] EveryTimeExecution;

        private float _currentTimeToCallExecution;
        
        public override void ExecuteInternal()
        {
            IsStarted = true;
            _currentTimeToCallExecution = TimeToCallExecution;
        }

        private void Update()
        {
            if (!IsStarted)
            {
                return;
            }

            _currentTimeToCallExecution -= Time.deltaTime;
            if (_currentTimeToCallExecution <= 0)
            {
                foreach (var execution in EveryTimeExecution)
                {
                    execution.Execute();
                }

                _currentTimeToCallExecution = TimeToCallExecution;
            }

            foreach (var execution in EveryTickExecution)
            {
                execution.Execute();
            }
        }
    }
}