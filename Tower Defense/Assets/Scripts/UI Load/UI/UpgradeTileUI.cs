using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeTileUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _price;
    private Button _button;
    [SerializeField] private EventTriggerButton _trigger;

    private void Start()
    {
        _canvas.worldCamera = ProjectContext.Instance.GameObjectsProvider.GameManager.Camera;
        _trigger.OnClick.AddListener(() =>
        {
            Debug.Log("ENABLED!!!");
            var enabled1 = !enabled;
            enabled = enabled1;
            gameObject.SetActive(enabled1);
        });
        enabled = false;
        gameObject.SetActive(false);
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        transform.localRotation = Quaternion.LookRotation(ProjectContext.Instance.GameObjectsProvider.GameManager.Camera.transform.position - transform.position);
    }

    public void SetPrice(int price) => _price.SetText(price.ToString());

    public void SetEvent(ISettable<int> pricer) =>
        _button.onClick.AddListener(() => pricer.Set(int.Parse(_price.text)));
}
