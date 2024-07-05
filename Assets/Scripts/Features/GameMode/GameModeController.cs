using System;
using UnityEngine;

namespace DefaultNamespace.Features.GameMode
{
    public class GameModeController : MonoBehaviour
    {
        public bool IsRoundStarted;
        public int RoundsCount;

        [Header("Conditions round end")]
        public bool IsAllOfConditions;
        public GameModeCondition[] Conditions;

        private void RoundStart()
        {
            
        }

        private void RoundComplete()
        {
            
        }
        
        private void GameStart()
        {
            
        }

        private void GameEnd()
        {
            
        }
        
        private void Update()
        {
            if (!IsRoundStarted)
            {
                return;
            }

            var isMatch = false;
            foreach (var condition in Conditions)
            {
                if (IsAllOfConditions)
                {
                    isMatch = condition.IsMatch();
                }
                else
                {
                    isMatch = true;
                    break;
                }
            }

            if (isMatch)
            {
                RoundComplete();
            }
        }
    }
}