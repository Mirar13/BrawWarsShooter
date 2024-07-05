
namespace Features.AI.AIStateMachine
{
    public interface IAIBlackboardFloat
    {
        public float GetFloat(string key);
        public void SetFloat(string key, float value);
    }
}