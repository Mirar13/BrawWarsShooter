using Features.GamePlayAbilityFeature.Components.PhysicsCast;
using UnityEngine;

public abstract class GamePlayCaster : MonoBehaviour
{
    public LayerMask CastLayers;
    public abstract void Cast();

    public abstract  (int hitCount, GamePlayCasterHitInfo[] infos) GetCasterHitInfos();
}
