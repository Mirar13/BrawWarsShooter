using UnityEngine;

namespace Features.GamePlayAbilityFeature.Components.PhysicsCast
{
    public abstract class GamePlayContainer : MonoBehaviour
    {
    }

    public abstract class GamePlayContainer<T> : GamePlayContainer
    {
        protected T _data;
        
        public void Set(T data)
        {
            _data = data;
        }

        public T Get()
        {
            return _data;
        } 
    }
}