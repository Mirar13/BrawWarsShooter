namespace Features.GamePlayAbilityFeature.Components.PhysicsCast
{
    public class GamePlayContainerCheckBoolExecution : GamePlayExecution
    {
        public GamePlayBoolContainer Container;
        public bool IsValue;
        public GamePlayExecution[] OnSuccess;
        public GamePlayExecution[] OnFailed;

        public override void ExecuteInternal()
        {
            var result = Container.Get();
            if (result != IsValue)
            {
                CallExecutions(OnFailed);
                return;
            }
            CallExecutions(OnSuccess);
        }

        private void CallExecutions(GamePlayExecution[] executions)
        {
            foreach (var execution in executions)
            {
                execution.Execute();
            }
        }
    }
}