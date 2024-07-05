using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Features.AboveHeadFeature
{
    public class AboveHeadUiHolder : MonoBehaviour
    {
        public static AboveHeadUiHolder Instance;
        public AboveHeadUiContainer Template;

        private Dictionary<GamePlayEntity, AboveHeadUiContainer> _containersByEntity =
            new Dictionary<GamePlayEntity, AboveHeadUiContainer>();
        
        private void Awake()
        {
            Instance = this;
        }

        public AboveHeadUiContainer RequestContainer(GamePlayEntity entity, float offsetY)
        {
            var container = Instantiate(Template, transform);
            container.Initialize((RectTransform)transform, entity, offsetY);
            _containersByEntity.Add(entity, container);
            return container;
        }

        public void RemoveContainer(GamePlayEntity entity)
        {
            if (!_containersByEntity.TryGetValue(entity, out var container))
            {
                return;
            }
            
            Destroy(container.gameObject);
            _containersByEntity.Remove(entity);
        }
    }
}