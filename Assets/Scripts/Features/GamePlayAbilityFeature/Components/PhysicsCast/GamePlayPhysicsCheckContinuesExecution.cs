namespace Features.GamePlayAbilityFeature.Components.PhysicsCast
{
    public class GamePlayPhysicsCheckContinuesExecution : GamePlayPhysicsCheckExecution
    {
        private bool _isExecuted;
        public override void ExecuteInternal()
        {
            _isExecuted = true;
        }

        private void Update()
        {
            if (!_isExecuted)
            {
                return;
            }
            
            Caster.Cast();
            (int hitCount, GamePlayCasterHitInfo[] infos) castInfo = Caster.GetCasterHitInfos();
            var hitsCount = castInfo.hitCount;
            if (hitsCount > 0)
            {
                Container.Set(castInfo.infos, hitsCount);
            
                foreach (var gamePlayComponent in OnAnyHitExecution)
                {
                    gamePlayComponent.Execute();
                }
            }
        }
    }
}