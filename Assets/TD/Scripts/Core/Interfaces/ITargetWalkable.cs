using UnityEngine;
using UnityEngine.Events;

public interface ITargetWalkable
{
    public void SetTarget(Point target);
    public void StopWalk();
}