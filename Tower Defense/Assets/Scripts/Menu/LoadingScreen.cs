using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _speedBar;
    private float _targetProgress;
    public async UniTask Load(Queue<ILoadingOperation> queue)
    {
        _canvas.worldCamera = ProjectContext.Instance.UiCamera;
        _canvas.enabled = true;
        StartCoroutine(UpdateSlider());
        
        foreach (var operation in queue)
        {
            ResetFill();
            _text.text = operation.Description;
            await operation.Load(OnProgress);
            await Wait();
        }
        
        _canvas.enabled = false;
    }

    private async UniTask Wait()
    {
        while (_slider.value < _targetProgress)
        {
            Debug.Log(_slider.value);
            await UniTask.Yield();
        }
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
    }

    private void OnProgress(float value)
    {
        _targetProgress = value;
    }

    private void ResetFill()
    {
        Debug.Log("ResetFill");
        _slider.value = 0;
        _targetProgress = 0;
    }

    private IEnumerator UpdateSlider()
    {
        Debug.Log("Slider is updating");
        while (_canvas.enabled)
        {
            Debug.Log(_targetProgress);
            if (_slider.value < _targetProgress)
                _slider.value += Time.deltaTime * _speedBar;
            yield return null;
        }
    }
}
