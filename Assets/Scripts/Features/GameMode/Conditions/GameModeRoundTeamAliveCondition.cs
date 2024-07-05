namespace DefaultNamespace.Features.GameMode.Conditions
{
    public class GameModeRoundTeamAliveCondition : GameModeCondition
    {
        public int TargetTeamAliveCount;
        
        public override bool IsMatch()
        {
            return false;
        }
    }
}