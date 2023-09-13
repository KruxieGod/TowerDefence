using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class SelectingTilesUI : MonoBehaviour
{
    [SerializeField] private ButtonInfo<EventTriggerButton>[] _buttons;

    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = ProjectContext.Instance.UiCamera;
    }

    public void Initialize(ISettable<TypeOfTile> settableFactory)
    {
        foreach (var buttonInfo in _buttons)
            buttonInfo._button.OnClick.AddListener(() => settableFactory.Set(buttonInfo._type));
    }
}

[Serializable]
public struct ButtonInfo<T>
{
    [field :SerializeField] public T _button { get; private set; }
    [field :SerializeField]  public TypeOfTile _type{ get; private set; }
}
