using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour, ITargetWalkable
{
    private NavMeshAgent _agent;
    private Point _walkTarget;
    private Map _map;
    private float _nextTargetDist = 3f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Init(Map map)
    {
        _map = map;
        SetTarget(_map.GetNext(_walkTarget, transform.position));
    }
    
    private void Update()
    {
        if (_walkTarget == null) return;

        var dist = Vector3.Distance(_walkTarget.transform.position, transform.position);
        if (dist < _nextTargetDist)
        {
            SetTarget(_map.GetNext(_walkTarget, transform.position));
        }
    }

    public void SetTarget(Point target)
    {
        _walkTarget = target;

        if (target == null)
        {
            return;
        }
        
        _agent.SetDestination(_walkTarget.transform.position);
    }

    public void StopWalk()
    {
        _agent.isStopped = true;
    }
}