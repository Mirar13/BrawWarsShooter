using DefaultNamespace.Features.ProjectGamePlay;
using Features.GamePlayAbilityFeature;
using UnityEngine;

namespace Features.AI.AIStateMachine
{
    public class AIAgent : GamePlayEntityComponent
    {
        public AIBlackboard Blackboard;
        public AIBehaviourTree behaviourTree;

        private GamePlayTargetFinderEntityComponent _targetFinderEntityComponent;
        
        public void Update()
        {
            behaviourTree.Tick(Time.deltaTime);
        }

        public override void InitializeInternal()
        {
            if (_entity.TryGetEntityComponent(out _targetFinderEntityComponent))
            {
                _targetFinderEntityComponent.OnTargetChanged += TargetFinderEntityComponentOnOnTargetChanged;
            }
        }
        
        private void OnDestroy()
        {
            _targetFinderEntityComponent.OnTargetChanged -= TargetFinderEntityComponentOnOnTargetChanged;
        }

        private void TargetFinderEntityComponentOnOnTargetChanged(bool hasTarget, GamePlayEntity target)
        {
            if (Blackboard is IAIBlackboardBool blackboardBool)
            {
                blackboardBool.SetBool("hasTarget", hasTarget);
            }
            
            if (hasTarget)
            {
                if (Blackboard is IAIBlackboardVector3 blackboardVector3)
                {
                    blackboardVector3.SetVector("targetPosition", target.transform.position);
                }
            }
        }
    }
}