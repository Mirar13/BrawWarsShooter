namespace Features.AI.AIStateMachine
{
    public interface IAIBlackboardBool
    {
        public bool GetBool(string key);
        public void SetBool(string key, bool value);
    }
}