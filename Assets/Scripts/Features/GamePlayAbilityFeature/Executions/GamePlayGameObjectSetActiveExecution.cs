using UnityEngine;

namespace Features.GamePlayAbilityFeature.Executions
{
    public class GamePlayGameObjectSetActiveExecution : GamePlayExecution
    {
        public GameObject TargetObject;
        public bool Active = false;
        
        public override void ExecuteInternal()
        {
            TargetObject.SetActive(Active);
        }
    }
}