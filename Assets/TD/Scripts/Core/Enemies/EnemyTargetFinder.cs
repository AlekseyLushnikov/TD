using System;
using UniRx;
using UnityEngine;

public class EnemyTargetFinder : MonoBehaviour, ITargetFinder<Wall>
{
    [SerializeField] private LayerMask _detectionLayer;
    [SerializeField] private float _targetDetectionRadius = 10f;

    public ReactiveProperty<Wall> Target { get; } = new ();

    public void TryFindTarget()
    {
        var hitColliders = new Collider[10];
        
        var numColliders = Physics.OverlapSphereNonAlloc(transform.position, _targetDetectionRadius, hitColliders, _detectionLayer);
        for (var i = 0; i < numColliders; i++)
        {
            Target.Value = hitColliders[i].GetComponent<Wall>();
        }
    }

    public void CheckTargetOutOfVision()
    {
        
    }

    private void Update()
    {
        if (Target.Value != null) return;
        TryFindTarget();
    }
}