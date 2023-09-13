using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventTriggerButton : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnClick;


    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
