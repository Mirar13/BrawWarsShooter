namespace Features.GamePlayAbilityFeature.Components.PhysicsCast
{
    public class GamePlayContainerSetBoolExecution : GamePlayExecution
    {
        public GamePlayBoolContainer Container;
        public bool Value;
        
        public override void ExecuteInternal()
        {
            Container.Set(Value);
        }
    }
}