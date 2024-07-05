using System.Collections;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Executions
{
    public class GamePlayTimeDelayExecution : GamePlayExecution
    {
        public float Delay;
        public GamePlayExecution[] Executions;

        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;
        
        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(Delay);
        }

        public override void ExecuteInternal()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(DelayCoroutine());
        }

        IEnumerator DelayCoroutine()
        {
            yield return _waitForSeconds;
            foreach (var execution in Executions)
            {
                execution.Execute();
            }
            _coroutine = null;
        }
    }
}