using System;
using System.Collections.Generic;
using DefaultNamespace.Characteristics;
using Features.GamePlayAbilityFeature;
using UnityEngine;

namespace DefaultNamespace.GameplayAbilitySystem
{
    public class GamePlayCharacteristicStorage : GamePlayEntityComponent
    {
        public CharacteristicsSettings Settings;
        private Dictionary<string, CharacteristicModel> _characteristicModels = new ();
        private List<ICharacteristicChangeHandler> _subscribers = new List<ICharacteristicChangeHandler>();

        public override void InitializeInternal()
        {
            foreach (var characteristicModel in Settings.Characteristics)
            {
                var modelCopy = new CharacteristicModel()
                {
                    Key = characteristicModel.Key,
                    BaseValue = characteristicModel.BaseValue
                };
                modelCopy.CurrentValue = modelCopy.BaseValue;
                modelCopy.MaxValue = modelCopy.BaseValue;
                modelCopy.OnCurrentValueChanged += ModelCopyOnCurrentValueChanged;
                _characteristicModels.Add(modelCopy.Key, modelCopy);
                ModelCopyOnCurrentValueChanged(modelCopy.Key, 0f, modelCopy.CurrentValue);
            }
        }
        
        public void RegisterHandler(ICharacteristicChangeHandler handler)
        {
            _subscribers.Add(handler);
        }

        public void UnregisterHandler(ICharacteristicChangeHandler handler)
        {
            _subscribers.Remove(handler);
        }

        private void ModelCopyOnCurrentValueChanged(string type, float previousValue, float currentValue)
        {
            foreach (var characteristicChangeHandler in _subscribers)
            {
                characteristicChangeHandler.CharacteristicChanged(type, previousValue, currentValue);
            }
        }
        
        public CharacteristicModel GetCharacteristicModel(string type)
        {
            if (!_characteristicModels.TryGetValue(type, out var model))
            {
                return null;
            }
            return model;
        }

        public float GetCharacteristicCurrentValue(string type)
        {
            if (!_characteristicModels.TryGetValue(type, out var model))
            {
                return 0f;
            }
            return model.CurrentValue;
        }
        
        public float GetCharacteristicMaxValue(string type)
        {
            if (!_characteristicModels.TryGetValue(type, out var model))
            {
                return 0f;
            }
            return model.MaxValue;
        }
    }
}