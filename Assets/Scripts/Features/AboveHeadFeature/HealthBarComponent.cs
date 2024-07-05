using System;
using System.Collections.Generic;
using DefaultNamespace.Characteristics;
using DefaultNamespace.GameplayAbilitySystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Features.AboveHeadFeature
{
    public class HealthBarComponent : MonoBehaviour, ICharacteristicChangeHandler
    {
        public TMP_Text AmountText;
        public Slider Slider;
        public Image FillImage;

        public Color ChangeGreateZeroColor;
        public Color ChangeLessZeroColor;

        public float TimeFade;
        public GameObject ChangeValueObject;
        public RectTransform ChangeValueRectTransform;
        public TMP_Text ChangeValueText;

        public string KeyValue = "health";

        private float _value;
        private float _maxValue;

        private bool _changeIsActive = false;
        private float _changeAmount = 0f;
        private float _timeToFade = 0f;
        
        private GamePlayCharacteristicStorage _characteristicStorage;
        
        public void Initialize(GamePlayEntity entity)
        {
            if (!entity.TryGetEntityComponent(out _characteristicStorage))
            {
                return;
            }

            _value = _characteristicStorage.GetCharacteristicCurrentValue(KeyValue);
            _maxValue = _characteristicStorage.GetCharacteristicMaxValue(KeyValue);
            
            UpdateVisual();
            _characteristicStorage.RegisterHandler(this);
        }

        private void OnDestroy()
        {
            _characteristicStorage.UnregisterHandler(this);
        }

        public void CharacteristicChanged(string type, float previousValue, float currentValue)
        {
            if (type == KeyValue)
            {
                _value = currentValue;
                
                _timeToFade = TimeFade;
                var previousIsActive = _changeIsActive;
                _changeIsActive = true;
                _changeAmount += currentValue - previousValue;
                ChangeValueText.text = _changeAmount.ToString();
                ChangeValueText.color = _changeAmount > 0f ? ChangeGreateZeroColor : ChangeLessZeroColor;
                ChangeValueObject.SetActive(true);
                if (!previousIsActive)
                {
                    var pos = ChangeValueRectTransform.anchoredPosition;
                    pos.y = 10f;
                    ChangeValueRectTransform.anchoredPosition = pos;
                    ChangeValueRectTransform.DOAnchorPosY(35, 1f);
                    ChangeValueText.CrossFadeAlpha(1f, 1f, true);
                }

                UpdateVisual();
            }
        }

        private void Update()
        {
            if (_changeIsActive)
            {
                _timeToFade -= Time.deltaTime;
                if (_timeToFade <= 0)
                {
                    _changeAmount = 0f;
                    _changeIsActive = false;

                    ChangeValueRectTransform.DOAnchorPosY(70, 1f)
                        .OnComplete(() => ChangeValueObject.SetActive(false));
                    ChangeValueText.CrossFadeAlpha(0f, 1f, true);
                }
            }
        }

        private void UpdateVisual()
        {
            AmountText.text = _value.ToString();
            Slider.value = _value / _maxValue;
        }
    }
}