using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.GameplayAbilitySystem.Effect
{
    public class GamePlayEffectOperation
    {
        private List<GamePlayEffectOperationPart> _operationParts;
        
        public void ApplyData(List<GamePlayEffectOperationPart> operationPartsSerialized)
        {
            _operationParts = new List<GamePlayEffectOperationPart>(operationPartsSerialized.Count);
            foreach (var operationPart in operationPartsSerialized)
            {
                _operationParts.Add(operationPart);
            }
        }
        
        public float Calculate(GamePlayCharacteristicStorage storage, GamePlayCharacteristicStorage executorStorage)
        {
            for (int i = 0; i < _operationParts.Count; i++)
            {
                _operationParts[i].Calculate(storage, executorStorage);
            }

            var result = _operationParts[_operationParts.Count - 1].Result;
            return result;
        }
    }
    
    [System.Serializable]
    public class GamePlayEffectOperationPart
    {
        public List<OperationSettingsEntry> Variables;
        public GamePlayEffectOperationType OperationType;

        public List<GamePlayEffectOperationPart> ConnectedParts;
        public bool isConnectedPartLeft;
        
        public float Result;

        public void Calculate(GamePlayCharacteristicStorage storage, GamePlayCharacteristicStorage executorStorage)
        {
            float a = 0;
            float b = 0;

            if (Variables.Count == 2)
            {
                var variableA = Variables[0];
                if (variableA.HasCharacteristicType)
                {
                    var selectedStorage = variableA.isExecutorCharacteristic ? executorStorage : storage;
                    var characteristicModel = selectedStorage.GetCharacteristicModel(variableA.CharacteristicType);
                    a = variableA.UseMaxValue ? characteristicModel.MaxValue : characteristicModel.CurrentValue;
                }
                else
                {
                    a = variableA.Value;
                }
                
                var variableB = Variables[1];
                if (variableB.HasCharacteristicType)
                {
                    var selectedStorage = variableB.isExecutorCharacteristic ? executorStorage : storage;
                    var characteristicModel = selectedStorage.GetCharacteristicModel(variableB.CharacteristicType);
                    b = variableB.UseMaxValue ? characteristicModel.MaxValue : characteristicModel.CurrentValue;
                }
                else
                {
                    b = variableB.Value;
                }
            }
            else if (Variables.Count == 1 && ConnectedParts.Count == 1)
            {
                var variable = Variables[0];
                if (variable.HasCharacteristicType)
                {
                    var selectedStorage = variable.isExecutorCharacteristic ? executorStorage : storage;
                    var characteristicModel = selectedStorage.GetCharacteristicModel(variable.CharacteristicType);
                    a = isConnectedPartLeft ? ConnectedParts[0].Result : variable.UseMaxValue ? characteristicModel.MaxValue : characteristicModel.CurrentValue;
                    b = isConnectedPartLeft ? variable.UseMaxValue ? characteristicModel.MaxValue : characteristicModel.CurrentValue: ConnectedParts[0].Result;
                }
                else
                {
                    a = isConnectedPartLeft ? ConnectedParts[0].Result : variable.Value;
                    b = isConnectedPartLeft ? variable.Value : ConnectedParts[0].Result;
                }
                
            }
            else if (Variables.Count == 1 && ConnectedParts.Count == 0)
            {
                var variable = Variables[0];
                if (variable.HasCharacteristicType)
                {
                    var selectedStorage = variable.isExecutorCharacteristic ? executorStorage : storage;
                    var characteristicModel = selectedStorage.GetCharacteristicModel(variable.CharacteristicType);
                    Result = variable.UseMaxValue ? characteristicModel.MaxValue : characteristicModel.CurrentValue;
                    return;
                }
                else
                {
                    Result = variable.Value;
                    return;
                }
            }
            else if (ConnectedParts.Count == 2)
            {
                a = ConnectedParts[0].Result;
                b = ConnectedParts[1].Result;
            }
            else
            {
                return;
            }
            
            switch (OperationType)
            {
                case GamePlayEffectOperationType.Add:
                    Result = a + b;
                    break;
                case GamePlayEffectOperationType.Subtract:
                    Result = a - b;
                    break;
                case GamePlayEffectOperationType.Divide:
                    Result = a / b;
                    break;
                case GamePlayEffectOperationType.Multiply:
                    Result = a * b;
                    break;
            }
        }
    }
}