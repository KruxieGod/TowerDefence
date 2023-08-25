using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    private Coroutine _activeCoroutine;
    private LineRenderer _lineRenderer;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1,Vector3.zero);
    }

    public void StrenchTo(Transform target,float timeAppear)
    {
        if (_activeCoroutine == null)
            _activeCoroutine = StartCoroutine(Strench(target, timeAppear));
        else
        {
            StopCoroutine(_activeCoroutine);
            _activeCoroutine = StartCoroutine(Strench(target, timeAppear));
        }
    }

    private IEnumerator Strench(Transform target,float timeAppear)
    {
        _lineRenderer.SetPosition(1,transform.InverseTransformPoint(target.position));
        yield return new WaitForSeconds(timeAppear);
        _lineRenderer.SetPosition(1,Vector3.zero);
    }
}
