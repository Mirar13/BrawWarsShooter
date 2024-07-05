using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] public RectTransform JoyStickRectTransform;
    [SerializeField] public RectTransform StickRectTransform;

    public Vector2 Velocities { get; private set; }
    private bool _isDragging;
    
    private float CalculateValue(float pointerPos, float bgPos, float bgSize, float stickSize)
    {
        return (pointerPos - bgPos) /
               ((bgSize - stickSize) / 2);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        var x = CalculateValue(eventData.position.x, JoyStickRectTransform.position.x, 
            JoyStickRectTransform.rect.size.x, StickRectTransform.rect.size.x);
        var y = CalculateValue(eventData.position.y, JoyStickRectTransform.position.y, 
            JoyStickRectTransform.rect.size.y, StickRectTransform.rect.size.y);
       
        var newPosition = new Vector3(x,y);
        newPosition = (newPosition.magnitude > 1f) ? newPosition.normalized :
            newPosition;

        Velocities = newPosition;
        UpdateVisual();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (_isDragging)
        {
            return;
        }
        Velocities = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
#endif
    
    private void UpdateVisual()
    {
        var visualX = Velocities.x*((JoyStickRectTransform.rect.size.x-StickRectTransform.rect.size.x)/2f);
        var visualY = Velocities.y*((JoyStickRectTransform.rect.size.y-StickRectTransform.rect.size.y)/2f);
        StickRectTransform.anchoredPosition = new Vector2(visualX,visualY);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        Velocities = Vector2.zero;
        UpdateVisual();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }
}
