using Features.AI.AIStateMachine;
using Features.GamePlayAbilityFeature;
using UnityEngine;

namespace DefaultNamespace.Features.ProjectGamePlay
{
    public class GamePlayAnimationEntityComponent : GamePlayEntityComponent
    {
        public Animator Animator;
        public float RunSpeed = 5f;
        
        private bool _isInitialized;
        private bool isAI;
        private AIAgent _aiAgent;
        
        private CharacterController _characterController;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int MoveVelocity = Animator.StringToHash("MoveVelocity");

        public override void InitializeInternal()
        {
            if (_entity.TryGetEntityComponent<GamePlayInputMoveEntityComponent>(
                    out var gamePlayInputMoveEntityComponent))
            {
                _characterController = gamePlayInputMoveEntityComponent.CharacterController;
            }
            else if(_entity.TryGetEntityComponent<AIAgent>(out var aiAgent))
            {
                _aiAgent = aiAgent;
                isAI = true;
            }

            _isInitialized = true;
        }
        
        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
            
            var velocity = Vector3.zero;
            if (isAI)
            {
                velocity = _aiAgent.Blackboard.Agent.velocity;
            }
            else
            {
                velocity = _characterController.velocity;
            }
            
            Animator.SetBool(IsMoving, velocity != Vector3.zero);
            Animator.SetFloat(MoveVelocity,  velocity.magnitude / RunSpeed);
        }
    }
}