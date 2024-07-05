using System;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Executions
{
    public class GamePlayTriggerStayHandleExecution : GamePlayExecution
    {
        public GamePlayExecution[] Executions;
        private bool _isExecuted;
        
        public override void ExecuteInternal()
        {
            _isExecuted = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isExecuted)
            {
                return;
            }
            
            foreach (var execution in Executions)
            {
                execution.Execute();
            }
        }
    }
}