using System.Collections.Generic;
using UnityEngine;

namespace Features.GamePlayAbilityFeature.Global
{
    public class GamePlayEntityCollisionHolder : MonoBehaviour
    {
        public static GamePlayEntityCollisionHolder _instance;

        public static GamePlayEntityCollisionHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("GamePlayEntityCollisionHolder").AddComponent<GamePlayEntityCollisionHolder>();
                }
                return _instance;
            }
        }
        
        public Dictionary<Collider, GamePlayEntity> _colliderToEntity = new ();
        public Dictionary<GamePlayEntity, Collider[]> _entityToColliders = new ();

        public void RegisterColliders(GamePlayEntity entity, Collider[] colliders)
        {
            _entityToColliders.Add(entity, colliders);
            foreach (var collider in colliders)
            {
                _colliderToEntity.Add(collider, entity);
            }
        }

        public void UnregisterColliders(GamePlayEntity entity)
        {
            if (!_entityToColliders.TryGetValue(entity, out var colliders))
            {
                return;
            }
            
            foreach (var collider in colliders)
            {
                _colliderToEntity.Remove(collider);
            }

            _entityToColliders.Remove(entity);
        }

        public bool TryGet(Collider collider, out GamePlayEntity result)
        {
            return _colliderToEntity.TryGetValue(collider, out result);
        }
    }
}