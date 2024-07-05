using System.Collections.Generic;
using DefaultNamespace.Characteristics;
using UnityEngine;

namespace DefaultNamespace.GameplayAbilitySystem.Effect
{
    public class GamePlayEffect
    {
        public GamePlayEntity Executor;
        public string[] TargetCharacteristicTypes;
        public GamePlayEffectOperationType OperandType;
        public GamePlayEffectOperation Operation = new ();

        public void ApplyData(GamePlayEntity executor, GamePlayEffectSettings settings)
        {
            Executor = executor;

            TargetCharacteristicTypes = settings.TargetCharacteristicTypes;
            OperandType = settings.OperationType;
            
            Operation = new GamePlayEffectOperation();
            Operation.ApplyData(settings.OperationPartsSerialized);
        }

        public void Calculate(GamePlayCharacteristicStorage target)
        {
            if(!Executor.TryGetEntityComponent<GamePlayCharacteristicStorage>(out var executorCharacteristicComponent))
            {
                return;
            }
            var value = Operation.Calculate(target, executorCharacteristicComponent);
            
            foreach (var type in TargetCharacteristicTypes)
            {
                var characteristicModel = target.GetCharacteristicModel(type);
                
                switch (OperandType)
                {
                    case GamePlayEffectOperationType.Add:
                        characteristicModel.CurrentValue += value;
                        break;
                    case GamePlayEffectOperationType.Subtract:
                        characteristicModel.CurrentValue -= value;
                        break;
                    case GamePlayEffectOperationType.Divide:
                        characteristicModel.CurrentValue /= value;
                        break;
                    case GamePlayEffectOperationType.Multiply:
                        characteristicModel.CurrentValue *= value;
                        break;
                }
                characteristicModel.CurrentValue = Mathf.Clamp(characteristicModel.CurrentValue, 0, characteristicModel.MaxValue);
            }
        }
    }
}