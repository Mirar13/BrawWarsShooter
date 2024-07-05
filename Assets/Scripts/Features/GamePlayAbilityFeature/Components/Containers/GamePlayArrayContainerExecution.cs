namespace Features.GamePlayAbilityFeature.Components.PhysicsCast
{
    public class GamePlayArrayContainerExecution : GamePlayExecution
    {
        public GamePlayArrayContainer Container;
        public GamePlayExecution[] OnElementExecutions;
        
        public override void ExecuteInternal()
        {
            var count = Container.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (var gamePlayExecution in OnElementExecutions)
                {
                    gamePlayExecution.Execute();
                }
                Container.Increment();
            }
        }
    }
}