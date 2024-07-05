using System.Collections.Generic;
using Features.AI.AIStateMachine;
using UnityEngine;

namespace Features.AI.Project.BloackBoard
{
    public class BotBlackboard : AIBlackboard, IAIBlackboardVector3, IAIBlackboardBool
    {
        private Dictionary<string, Vector3> _vectorsByKey = new Dictionary<string, Vector3>();
        private Dictionary<string, bool> _boolValues = new Dictionary<string, bool>();

        public Vector3 GetVector(string key)
        {
            if (_vectorsByKey.TryGetValue(key, out var vector))
            {
                return vector;
            }

            return Vector3.zero;
        }

        public void SetVector(string key, Vector3 value)
        {
            _vectorsByKey[key] = value;
        }

        public bool GetBool(string key)
        {
            if (!_boolValues.TryGetValue(key, out var result))
            {
                return false;
            }
            return result;
        }

        public void SetBool(string key, bool value)
        {
            _boolValues[key] = value;
        }
    }
}