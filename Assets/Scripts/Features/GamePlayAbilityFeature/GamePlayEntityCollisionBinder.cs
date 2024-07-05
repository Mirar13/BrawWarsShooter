using System;
using Features.GamePlayAbilityFeature.Global;
using UnityEngine;

namespace Features.GamePlayAbilityFeature
{
    public class GamePlayEntityCollisionBinder : GamePlayEntityComponent
    {
        public Collider[] Colliders;
        
        public override void InitializeInternal()
        {
            GamePlayEntityCollisionHolder.Instance.RegisterColliders(_entity, Colliders);
        }

        private void OnDestroy()
        {
            GamePlayEntityCollisionHolder.Instance.UnregisterColliders(_entity);
        }
    }
}