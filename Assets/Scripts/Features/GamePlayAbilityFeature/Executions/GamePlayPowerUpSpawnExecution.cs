using System;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Executions
{
    public class GamePlayPowerUpSpawnExecution : GamePlayExecution
    {
        public GamePlayEntity PowerUpEntityPrefab;
        public Transform SpawnPoint;
        public float TimeToRespawn;

        private GamePlayEntity _instancedEntity;
        private bool _hasEntity;
        private float _currentTimeToRespawn;
        
        public override void ExecuteInternal()
        {
            Spawn();
        }

        private void OnDestroy()
        {
            if (_hasEntity)
            {
                Destroy(_instancedEntity.gameObject);
                _instancedEntity = null;
                _hasEntity = false;
            }
        }

        private void Spawn()
        {
            var entity = Instantiate(PowerUpEntityPrefab, SpawnPoint);
            entity.StartEntity();
            entity.OnEntityDestroy += EntityOnEntityDestroy;
            _instancedEntity = entity;
            _hasEntity = true;
        }

        private void EntityOnEntityDestroy()
        {
            _instancedEntity = null;
            _hasEntity = false;
        }

        private void Update()
        {
            if (_hasEntity)
            {
                return;
            }

            if (_currentTimeToRespawn <= TimeToRespawn)
            {
                _currentTimeToRespawn += Time.deltaTime;
                return;
            }
            
            Spawn();
            _currentTimeToRespawn = 0;
        }
    }
}