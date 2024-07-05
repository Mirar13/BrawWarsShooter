using System;
using DefaultNamespace.Characteristics;
using DefaultNamespace.GameplayAbilitySystem;
using Features.GamePlayAbilityFeature;

namespace DefaultNamespace.Features.ProjectGamePlay
{
    public class GamePlayDeathHandlerEntityComponent : GamePlayEntityComponent, ICharacteristicChangeHandler
    {
        private GamePlayCharacteristicStorage _characteristicStorage;
        private bool _isInitialized;
        
        public override void InitializeInternal()
        {
            _entity.TryGetEntityComponent(out _characteristicStorage);
            _characteristicStorage.RegisterHandler(this);
            _isInitialized = true;
        }

        private void OnDestroy()
        {
            _characteristicStorage.UnregisterHandler(this);
        }

        public void CharacteristicChanged(string type, float previousValue, float currentValue)
        {
            if (type != "health")
            {
                return;
            }

            if (currentValue > 0)
            {
                return;
            }

            Destroy(_entity.gameObject);
        }
    }
}