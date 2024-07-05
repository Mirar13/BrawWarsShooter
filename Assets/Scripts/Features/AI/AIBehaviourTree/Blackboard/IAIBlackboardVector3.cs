using UnityEngine;

namespace Features.AI.AIStateMachine
{
    public interface IAIBlackboardVector3
    {
        public Vector3 GetVector(string key);
        public void SetVector(string key, Vector3 value);
    }
}