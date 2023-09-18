using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class TestJobs : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    void Start()
    {
        TransformAccessArray arrayTransform = new(10);
        var hits = new NativeArray<RaycastHit>(10,Allocator.TempJob);
        var commands = new NativeArray<RaycastCommand>(10,Allocator.TempJob);
        for (int i = 0; i < 10; i++)
        {
            var obj = Instantiate(_gameObject, transform.position, Quaternion.identity).transform;
            arrayTransform.Add(obj);
            commands[i] = new(obj.position,Vector3.down);
        }
        var handle = RaycastCommand.ScheduleBatch(commands, hits, 10);
        NativeArray<Vector3> sizes = new(arrayTransform.length,Allocator.TempJob);
        handle.Complete();
        var job = new TransformJob(){Sizes = sizes,Hits = hits};
        var j = job.Schedule(arrayTransform);
        j.Complete();
    }
}

public struct TransformJob: IJobParallelForTransform
{
    public NativeArray<Vector3> Sizes;
    public NativeArray<RaycastHit> Hits;
    public void Execute(int index, TransformAccess transform)
    {
        if (Hits[index].transform.TryGetComponent(out Collider collider))
        {
            Sizes[index] = collider.transform.localScale;
        }
    }
}
