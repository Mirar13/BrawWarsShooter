namespace DefaultNamespace.Features.GameMode.Conditions
{
    public class GameModeRoundTimeCondition : GameModeCondition
    {
        public float TargetTime;
        
        public override bool IsMatch()
        {
            return false;
        }
    }
}