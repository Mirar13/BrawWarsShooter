namespace Features.GamePlayAbilityFeature.Executions
{
    public class GamePlayStopUpdateExecution : GamePlayExecution
    {
        public GamePlayUpdateExecution UpdateExecution;

        public override void ExecuteInternal()
        {
            UpdateExecution.IsStarted = false;
        }
    }
}