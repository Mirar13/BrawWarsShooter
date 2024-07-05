using System;
using System.Collections.Generic;
using DefaultNamespace.Characteristics;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.GameplayAbilitySystem.Effect
{
    [CreateAssetMenu(fileName = "EffectSettings", menuName = "SO/Effect")]
    public class GamePlayEffectSettings : ScriptableObject
    {
        private const char BraceOpen = '(';
        private const char BraceClose = ')';
        private const string IndexPart = "i:";
        private static readonly char[] OperationSymbols = new char[] { '+', '-', '/', '*' };
        
        [Header("Apply settings")]
        public GamePlayEffectDurationType DurationType;
        [Tooltip("Характеристика из которой брать время продолжительности")] 
        public string DurationCharacteristicType;
        [Tooltip("Может применяться несколько раз за время действия")] 
        public bool ApplyMultiplyTimes;
        [Tooltip("Время между применениями")] 
        public float Interval;

        [Header("Operation settings")]
        public string[] TargetCharacteristicTypes;
        public GamePlayEffectOperationType OperationType;
        [FormerlySerializedAs("Operations")] public GamePlayEffectOperationSettings Operation;

        [HideInInspector] public List<GamePlayEffectOperationPart> OperationPartsSerialized = new ();

#if UNITY_EDITOR
        private void OnValidate()
        {
            OperationPartsSerialized.Clear();
            var operationStr = Operation.OperationStr;
            var opStr = operationStr;
            var countBrackets = 0;
            for (int i = 0; i < opStr.Length; i++)
            {
                if (opStr[i] == BraceOpen)
                {
                    countBrackets++;
                }
            }
            
            for (int i = 0; i < countBrackets; i++)
            {
                var opStartIndex = opStr.LastIndexOf(BraceOpen);
                var opEndIndex = -1;
                for (int j = opStartIndex; j < opStr.Length; j++)
                {
                    if (opStr[j] == BraceClose)
                    {
                        opEndIndex = j;
                        break;
                    }
                }

                if (opEndIndex != -1)
                {
                    var startIndex = opStartIndex + 1;
                    var count = (opEndIndex-opStartIndex)-1;

                    var currOperation = opStr.Substring(startIndex, count);
                    
                    var part = new GamePlayEffectOperationPart();
                    var variables = currOperation.Split(OperationSymbols);
                    if (variables.Length > 1)
                    {
                        var opSymbol = currOperation[variables[0].Length];
                        switch (opSymbol)
                        {
                            case '+':
                                part.OperationType = GamePlayEffectOperationType.Add;
                                break;
                            case '-':
                                part.OperationType = GamePlayEffectOperationType.Subtract;
                                break;
                            case '/':
                                part.OperationType = GamePlayEffectOperationType.Divide;
                                break;
                            case '*':
                                part.OperationType = GamePlayEffectOperationType.Multiply;
                                break;
                        }
                    }

                    part.Variables = new List<OperationSettingsEntry>(2);
                    part.ConnectedParts = new List<GamePlayEffectOperationPart>(2);
                    for (int j = 0; j < variables.Length; j++)
                    {
                        var variable = variables[j];
                        var partIndex = variable.IndexOf(IndexPart);
                        if (partIndex >= 0)
                        {
                            var index = int.Parse(variable.Substring(partIndex + 2, 1));
                            part.ConnectedParts.Add(OperationPartsSerialized[index]);
                            if (j == 0)
                            {
                                part.isConnectedPartLeft = true;
                            }
                        }
                        else
                        {
                            var operationSettingsEntry = Operation.OperationEntriesByKey[variables[j]];
                            part.Variables.Add(operationSettingsEntry);
                        }
                    }
                    OperationPartsSerialized.Add(part);

                    opStr = opStr.Remove(startIndex-1, count+2);
                    opStr = opStr.Insert(startIndex-1, $"{IndexPart}{i}");
                }
            }
        }
#endif
    }
}