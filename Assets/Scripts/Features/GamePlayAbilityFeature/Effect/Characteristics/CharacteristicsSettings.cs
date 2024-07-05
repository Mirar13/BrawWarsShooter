using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Characteristics
{
    [CreateAssetMenu(menuName = "SO/Characteristics", fileName = "New Characteristics")]
    public class CharacteristicsSettings : ScriptableObject
    {
        [SerializeField] public List<CharacteristicModel> Characteristics;
    }
}