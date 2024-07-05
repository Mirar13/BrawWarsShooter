using Features.GamePlayAbilityFeature.Components.PhysicsCast;

public class GamePlayPhysicsCheckExecution : GamePlayExecution
{
    public GamePlayCaster Caster;
    public GamePlayCasterHitInfoContainer Container;
    private bool _hasContainer;
    
    public GamePlayExecution[] OnAnyHitExecution;
    
    public override void ExecuteInternal()
    {
        ExecuteCast();
    }

    public virtual void ExecuteCast()
    {
        Caster.Cast();
        (int hitCount, GamePlayCasterHitInfo[] infos) castInfo = Caster.GetCasterHitInfos();
        var hitsCount = castInfo.hitCount;
        if (hitsCount > 0)
        {
            if (_hasContainer)
            {
                Container.Set(castInfo.infos, hitsCount);
            }

            foreach (var gamePlayComponent in OnAnyHitExecution)
            {
                gamePlayComponent.Execute();
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _hasContainer = Container != null;
    }
#endif
}
