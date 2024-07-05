using System;
using DefaultNamespace.Characteristics;
using DefaultNamespace.GameplayAbilitySystem;
using Features.GamePlayAbilityFeature;
using UnityEngine;

public class GamePlayInputMoveEntityComponent : GamePlayEntityComponent, ICharacteristicChangeHandler
{
    public CharacterController CharacterController;
    public string MoveSpeedName;
    
    private JoyStick _joyStick;
    private GamePlayCharacteristicStorage _characteristicStorage;
    
    private bool _isInitialized;
    private float _moveSpeed;
    
    
    public override void InitializeInternal()
    {
        _joyStick = FindObjectOfType<JoyStick>();
        
        if (_entity.TryGetEntityComponent<GamePlayCharacteristicStorage>(out var component))
        {
            _characteristicStorage = component;
            _characteristicStorage.RegisterHandler(this);
            _moveSpeed = _characteristicStorage.GetCharacteristicCurrentValue(MoveSpeedName);
        }
        _isInitialized = true;
    }

    private void OnDestroy()
    {
        _characteristicStorage.UnregisterHandler(this);
    }

    private void Update()
    {
        if (!_isInitialized)
        {
            return;
        }

        var velocityInput = _joyStick.Velocities;
        var velocity = velocityInput * _moveSpeed;
        CharacterController.Move(new Vector3(velocity.x, 0, velocity.y)*Time.deltaTime);

        if (velocityInput != Vector2.zero)
        {
            var currentPos = transform.position;
            currentPos.y = 0;
            var targetPos = currentPos + new Vector3(velocityInput.x, 0, velocityInput.y);
            transform.rotation = Quaternion.LookRotation(targetPos - currentPos);
        }
    }

    public void CharacteristicChanged(string type, float previousValue, float currentValue)
    {
        if (type != MoveSpeedName)
        {
            return;
        }

        _moveSpeed = currentValue;
    }
}
