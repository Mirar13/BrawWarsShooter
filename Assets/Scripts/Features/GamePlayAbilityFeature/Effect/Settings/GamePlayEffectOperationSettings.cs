using System.Collections.Generic;
using DefaultNamespace.Characteristics;

namespace DefaultNamespace.GameplayAbilitySystem.Effect
{
    [System.Serializable]
    public class GamePlayEffectOperationSettings
    {
        public string OperationStr;
        public OperationSettingsEntry[] OperationEntries;

        private Dictionary<string, OperationSettingsEntry> _operationEntriesByKey;
        public Dictionary<string, OperationSettingsEntry> OperationEntriesByKey
        {
            get
            {
                if (_operationEntriesByKey == null)
                {
                    _operationEntriesByKey = new Dictionary<string, OperationSettingsEntry>(OperationEntries.Length);
                    foreach (var operationEntry in OperationEntries)
                    {
                        _operationEntriesByKey.Add(operationEntry.Key, operationEntry);
                    }
                }
#if UNITY_EDITOR
                var result = _operationEntriesByKey;
                _operationEntriesByKey = null;
                return result;
#endif
                return _operationEntriesByKey;
            }
        }
    }

    [System.Serializable]
    public struct OperationSettingsEntry
    {
        public string Key;
        public bool isExecutorCharacteristic;
        public bool HasCharacteristicType;
        public string CharacteristicType;
        public bool UseMaxValue;
        public float Value;
    }
}