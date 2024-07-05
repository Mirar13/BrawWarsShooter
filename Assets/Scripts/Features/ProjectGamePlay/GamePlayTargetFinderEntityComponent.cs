using System;
using DefaultNamespace.Characteristics;
using DefaultNamespace.GameplayAbilitySystem;
using Features.GamePlayAbilityFeature;
using Features.GamePlayAbilityFeature.Global;
using Unity.Jobs;

namespace DefaultNamespace.Features.ProjectGamePlay
{
    public class GamePlayTargetFinderEntityComponent : GamePlayEntityComponent, ICharacteristicChangeHandler
    {
        public event Action<bool, GamePlayEntity> OnTargetChanged;
        public string CharacteristicRangeKey = "attackRange";
        private GamePlayCharacteristicStorage _characteristicStorage;
        
        private bool _isInitialized;

        private bool _hasJob;
        private JobHandle _jobHandle;
        private ClosestFindIndexJob _job;
        
        private float _targetRange;
        
        public GamePlayEntity CurrentTarget;
        public bool HasTarget = false;

        public override void InitializeInternal()
        {
            _entity.TryGetEntityComponent(out _characteristicStorage);
            if (_entity.TryGetEntityComponent<GamePlayCharacteristicStorage>(out var component))
            {
                _characteristicStorage = component;
                _characteristicStorage.RegisterHandler(this);
                _targetRange = _characteristicStorage.GetCharacteristicCurrentValue(CharacteristicRangeKey);
            }
            _isInitialized = true;
        }
        
        private void OnDestroy()
        {
            _characteristicStorage.UnregisterHandler(this);
        }
        
        public void CharacteristicChanged(string type, float previousValue, float currentValue)
        {
            if (type != CharacteristicRangeKey)
            {
                return;
            }

            _targetRange = currentValue;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            if (!_hasJob)
            {
                var jobInfo = GamePlayEntitiesHolder.Instance.GetClosest(_entity, _targetRange);
                _jobHandle = jobInfo.jobHandle;
                _job = jobInfo.job;
                _hasJob = true;
            }
        }

        private void LateUpdate()
        {
            if (_hasJob)
            {
                _jobHandle.Complete();
                if (_jobHandle.IsCompleted)
                {
                    var foundedTeamIndex = _job.Result[0];
                    var foundedIndex = _job.Result[1];
                    if (foundedTeamIndex != -1 && foundedIndex != -1)
                    {
                        if (GamePlayEntitiesHolder.Instance.TryGetByIndex(foundedTeamIndex, foundedIndex,
                                out var result))
                        {
                            CurrentTarget = result;
                            CurrentTarget.OnEntityDestroy += CurrentTargetOnEntityDestroy;
                            HasTarget = true;
                            OnTargetChanged?.Invoke(true, CurrentTarget);
                        }
                    }
                    else
                    {
                        if (HasTarget)
                        {
                            OnTargetChanged?.Invoke(false, null);
                            CurrentTarget.OnEntityDestroy -= CurrentTargetOnEntityDestroy;
                        }
                        CurrentTarget = null;
                        HasTarget = false;
                    }

                    _job.Result.Dispose();
                    _job.TeamEndIndexes.Dispose();
                    _job.PositionsByTeams.Dispose();

                    _hasJob = false;
                }
            }
        }

        private void CurrentTargetOnEntityDestroy()
        {
            CurrentTarget = null;
            HasTarget = false;
        }
    }
}