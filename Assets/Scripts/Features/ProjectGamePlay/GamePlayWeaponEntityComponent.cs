using DefaultNamespace.Characteristics;
using DefaultNamespace.Features.ProjectGamePlay;
using DefaultNamespace.GameplayAbilitySystem;
using Features.AI.AIStateMachine;
using Features.GamePlayAbilityFeature;
using UnityEngine;

public class GamePlayWeaponEntityComponent : GamePlayEntityComponent, ICharacteristicChangeHandler
{
    public Transform AimPoint;
    public GamePlayEntity ProjectilePrefab;
    public string AttackCooldownKey;
    public float RotationSpeed;
    
    private GamePlayTargetFinderEntityComponent _targetFinderEntityComponent;
    private GamePlayInputMoveEntityComponent _gamePlayInputMoveEntityComponent;
    private GamePlayCharacteristicStorage _characteristicStorage;
    private bool _isInitialized;

    private bool isAI;
    private AIAgent _aiAgent;

    private float _attackCooldown;
    private float _currentCooldown;
    
    public override void InitializeInternal()
    {
        _entity.TryGetEntityComponent(out _targetFinderEntityComponent);
        _isInitialized = true;

        _aiAgent = GetComponent<AIAgent>();
        isAI = _aiAgent != null;
        if (!isAI)
        {
            _entity.TryGetEntityComponent(out _gamePlayInputMoveEntityComponent);
        }
        
        if (_entity.TryGetEntityComponent(out _characteristicStorage))
        {
            _characteristicStorage.RegisterHandler(this);
            _attackCooldown = _characteristicStorage.GetCharacteristicCurrentValue(AttackCooldownKey);
        }
    }
    
    private void OnDestroy()
    {
        _characteristicStorage.UnregisterHandler(this);
    }

    void RotateToTarget()
    {
        if (!_targetFinderEntityComponent.HasTarget)
        {
            return;
        }
        var target = _targetFinderEntityComponent.CurrentTarget.transform;
        var targetPos = target.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation((targetPos - transform.position)), RotationSpeed * Time.deltaTime);
    }
    
    private void Update()
    {
        if (!_isInitialized)
        {
            return;
        }
        
        bool isMoving = false;

        if (isAI)
        {
            isMoving = _aiAgent.Blackboard.Agent.velocity != Vector3.zero;
        }
        else
        {
            isMoving = _gamePlayInputMoveEntityComponent.CharacterController.velocity != Vector3.zero;
        }

        if (!isMoving)
        {
            RotateToTarget();
        }
        else
        {
            if (_currentCooldown <= _attackCooldown)
            {
                _currentCooldown += Time.deltaTime;
            }
            
            return;
        }

        if (_currentCooldown <= _attackCooldown)
        {
            _currentCooldown += Time.deltaTime;
            return;
        }

        if (!_targetFinderEntityComponent.HasTarget)
        {
            return;
        }
        
        var target = _targetFinderEntityComponent.CurrentTarget.transform;
        var targetPos = target.position;
        AimPoint.LookAt(targetPos);
        var bullet =Instantiate(ProjectilePrefab, AimPoint.transform.position, Quaternion.LookRotation(AimPoint.forward), null);
        bullet.TryGetEntityComponent<GamePlayCharacteristicStorage>(out var _bulletCharacteristics);
        _bulletCharacteristics.Settings = _characteristicStorage.Settings;
        bullet.StartEntity();
        _currentCooldown = 0;
    }

    public void CharacteristicChanged(string type, float previousValue, float currentValue)
    {
        if (type != AttackCooldownKey)
        {
            return;
        }

        _attackCooldown = currentValue;
    }
}
