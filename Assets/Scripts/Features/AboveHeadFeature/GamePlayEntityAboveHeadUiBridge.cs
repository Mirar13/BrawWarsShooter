using System;
using DefaultNamespace.Features.AboveHeadFeature;
using Features.GamePlayAbilityFeature;

public class GamePlayEntityAboveHeadUiBridge : GamePlayEntityComponent
{
    public float OffsetY;
    private AboveHeadUiContainer _container;
    
    public override void InitializeInternal()
    {
        _container = AboveHeadUiHolder.Instance.RequestContainer(_entity, OffsetY);
    }

    private void OnDestroy()
    {
        AboveHeadUiHolder.Instance.RemoveContainer(_entity);
    }
}
