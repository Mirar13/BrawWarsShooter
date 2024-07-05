namespace Features.GamePlayAbilityFeature.Components.Debug
{
    public class GamePlayDebugLogExecution : GamePlayExecution
    {
        public string Log;
        
        public override void ExecuteInternal()
        {
            UnityEngine.Debug.Log(Log);
        }
    }
}