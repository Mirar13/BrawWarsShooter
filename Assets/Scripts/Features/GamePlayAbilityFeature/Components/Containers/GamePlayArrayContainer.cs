using Features.GamePlayAbilityFeature.Components.PhysicsCast.Interfaces;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Components.PhysicsCast
{
    public abstract class GamePlayArrayContainer : GamePlayContainer
    {
        public int Count { get; protected set; }
        protected int _currentIndex;

        public void Increment()
        {
            if (Count <= 0 || _currentIndex >= Count-1)
            {
                return;
            }

            _currentIndex++;
        }
    }
    
    public abstract class GamePlayArrayContainer<T> : GamePlayArrayContainer
    {
        protected T[] _data;
        
        public void Set(T[] data, int length = 0)
        {
            _data = data;
            Count = length;
            _currentIndex = 0;
        }

        public bool Get(out T result)
        {
            result = default;
            if (Count <= 0 || _currentIndex >= Count)
            {
                return false;
            }
            result = _data[_currentIndex];
            return true;
        }
    }
}