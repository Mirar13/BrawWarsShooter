using System;
using System.Collections.Generic;
using Features.GamePlayAbilityFeature;
using Features.GamePlayAbilityFeature.Global;
using UnityEngine;

public class GamePlayEntity : MonoBehaviour
{
    public event Action OnEntityDestroy;
    
    public bool ItsSelfStarted = false;
    public bool RegisterInHolder = false;
    public GamePlayExecution[] OnStartExecutions;
    
    [SerializeField] private GamePlayExecution[] _childComponents;
    [SerializeField] private GamePlayEntityComponent[] _childEntityComponents;

    private Dictionary<Type, GamePlayEntityComponent> _childEntityComponentsByType = new ();

    private void Awake()
    {
        _childEntityComponentsByType.Clear();
        foreach (var childEntityComponent in _childEntityComponents)
        {
            _childEntityComponentsByType.Add(childEntityComponent.GetType(), childEntityComponent);
        }
    }

    private void Start()
    {
        if (ItsSelfStarted)
        {
            StartEntity();
        }
    }

    public void StartEntity()
    {
        foreach (var childEntityComponent in _childEntityComponents)
        {
            childEntityComponent.Initialize(this);
        }
        
        foreach (var childExecution in _childComponents)
        {
            childExecution.Initialize(this);
        }
        
        foreach (var gamePlayComponent in OnStartExecutions)
        {
            gamePlayComponent.Execute();
        }

        if (RegisterInHolder)
        {
            GamePlayEntitiesHolder.Instance.Register(this);
        }
    }

    private void OnDestroy()
    {
        GamePlayEntitiesHolder.Instance.Unregister(this);
        OnEntityDestroy?.Invoke();
    }

    public bool TryGetEntityComponent<T>(out T result) where T : GamePlayEntityComponent
    {
        result = null;
        if (!_childEntityComponentsByType.TryGetValue(typeof(T), out var component))
        {
            return false;
        }
        result = (T)component;
        return true;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _childComponents = gameObject.GetComponentsInChildren<GamePlayExecution>();
        _childEntityComponents = gameObject.GetComponentsInChildren<GamePlayEntityComponent>();
    }
#endif
}
