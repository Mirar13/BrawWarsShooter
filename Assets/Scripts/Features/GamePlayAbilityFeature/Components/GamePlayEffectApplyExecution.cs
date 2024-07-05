using DefaultNamespace.GameplayAbilitySystem;
using DefaultNamespace.GameplayAbilitySystem.Effect;
using Features.GamePlayAbilityFeature.Components.GetterProcessors;
using UnityEngine.Serialization;

public class GamePlayEffectApplyExecution : GamePlayExecution
{
    public GamePlayEffectSettings Effect;
    public GamePlayHitInfoEntityContainerGetter hitInfoEntityContainerGetter;
    
    public override void ExecuteInternal()
    {
        var entity = hitInfoEntityContainerGetter.GetEntity();
        var effectInstance = new GamePlayEffect();
        effectInstance.ApplyData(_entity, Effect);

        entity.TryGetEntityComponent<GamePlayCharacteristicStorage>(out var targetCharacteristicStorager);
        effectInstance.Calculate(targetCharacteristicStorager);
    }
}
