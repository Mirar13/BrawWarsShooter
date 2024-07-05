using UnityEngine;

namespace Features.GamePlayAbilityFeature
{
    public abstract class GamePlayEntityComponent : MonoBehaviour
    {
        protected GamePlayEntity _entity;
        
        public void Initialize(GamePlayEntity entity)
        {
            _entity = entity;
            InitializeInternal();
        }

        public abstract void InitializeInternal();
    }
}