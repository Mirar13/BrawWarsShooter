using System;
using UnityEngine;

namespace DefaultNamespace.Characteristics
{
    [System.Serializable]
    public class CharacteristicModel
    {
        public event Action<string, float, float> OnCurrentValueChanged;
        public string Key;
        public float BaseValue;
        public float MaxValue;

        private float _currentValue;
        
        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                var prevValue = _currentValue;
                _currentValue = value;
                OnCurrentValueChanged?.Invoke(Key, prevValue, _currentValue);
            }
        }
    }
}