using System;
using UniRx;
using UnityEngine;

public class TowerTargetFounder : MonoBehaviour, ITargetFinder<Enemy>
{
    [SerializeField] private float _targetDetectionRadius = 5f;
    [SerializeField] private float _targetLostDistance = 10f;
    [SerializeField] private LayerMask _detectionLayer;

    public ReactiveProperty<Enemy> Target { get; } = new ();

    private IDisposable _disposable;

    public void TryFindTarget()
    {
        var hitColliders = new Collider[10];
        var numColliders =
            Physics.OverlapSphereNonAlloc(transform.position, _targetDetectionRadius, hitColliders, _detectionLayer);
        for (var i = 0; i < numColliders; i++)
        {
            Target.Value = hitColliders[i].GetComponent<Enemy>();
            Target.Value.IsEliminated.Where(x => x).Subscribe(_ => Clear()).AddTo(this);
            return;
        }
    }

    private void Clear()
    {
        _disposable?.Dispose();
        Target.Value = null;
    }

    public void CheckTargetOutOfVision()
    {
        if (Vector3.Distance(transform.position, Target.Value.Transform.position) > _targetLostDistance)
        {
            Clear();
        }
    }
    
    private void Update()
    {
        if (Target.Value != null)
        {
            CheckTargetOutOfVision();
        }
        else
        {
            TryFindTarget();
        }
    }

}