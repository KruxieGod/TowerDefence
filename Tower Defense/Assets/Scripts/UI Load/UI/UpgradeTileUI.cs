using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EventTriggerButton))]
public class UpgradeTileUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _price;
    private EventTriggerButton _button;
    [SerializeField] private EventTriggerButton _trigger;

    private void Awake()
    {
        _canvas.worldCamera = ProjectContext.Instance.UiCamera;
        _trigger.OnClick.AddListener(() =>
        {
            Debug.Log("ENABLED!!!");
            var enabled1 = !enabled;
            enabled = enabled1;
            gameObject.SetActive(enabled1);
        });
        enabled = false;
        gameObject.SetActive(false);
        _button = GetComponent<EventTriggerButton>();
    }

    private void Update()
    {
        transform.localRotation = Quaternion.LookRotation(ProjectContext.Instance.GameObjectsProvider.GameManager.Camera.transform.position - transform.position);
    }

    public void OnClick(Action action) => _button.OnClick.AddListener(action.Invoke);
    public void SetPrice(int price) => _price.SetText(price.ToString());
    public void SetEvent(ISettable<int> pricer) =>
        _button.OnClick.AddListener(() => pricer.Set(int.Parse(_price.text)));
}
