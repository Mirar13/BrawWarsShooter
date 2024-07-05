using System;
using UnityEngine;

namespace DefaultNamespace.Features.AboveHeadFeature
{
    public class AboveHeadUiContainer : MonoBehaviour
    {
        public HealthBarComponent HealthBar;
        private GamePlayEntity _entity;
        private float _offsetY;

        private RectTransform _rectTransform;
        private RectTransform _holderTransform;
        
        public void Initialize(RectTransform holder, GamePlayEntity entity, float offsetY)
        {
            _holderTransform = holder;
            _entity = entity;
            _offsetY = offsetY;
            HealthBar.Initialize(_entity);
            _rectTransform = (RectTransform)transform;
        }

        private void Update()
        {
            var newPosition = _holderTransform.InverseTransformPoint(Camera.main.WorldToScreenPoint(_entity.transform.position));
            newPosition.z = 0;
            newPosition.y += _offsetY;
            _rectTransform.anchoredPosition = newPosition;
        }
    }
}