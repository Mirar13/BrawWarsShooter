using Features.GamePlayAbilityFeature.Components.PhysicsCast;
using Features.GamePlayAbilityFeature.Components.PhysicsCast.Interfaces;
using Features.GamePlayAbilityFeature.Global;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Components.GetterProcessors
{
    public class GamePlayHitInfoEntityContainerGetter : MonoBehaviour
    {
        public GamePlayArrayContainer ArrayContainer;

        public GamePlayEntity GetEntity()
        {
            if (ArrayContainer is not GamePlayCasterHitInfoContainer hitInfoContainer)
            {
                return null;
            }

            if (!hitInfoContainer.Get(out var result))
            {
                return null;
            }
            
            if(!GamePlayEntityCollisionHolder.Instance.TryGet(result.Collider, out var entity))
            {
                return null;
            }
            
            return entity;
        }
    }
}