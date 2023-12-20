using UniRx;
using UnityEngine;

public interface IDamageable
{
    public void SetDamage(int damage);
    public IReactiveProperty<int> Health { get;}
    public IReadOnlyReactiveProperty<bool> IsEliminated { get; }
    public void OnZeroHealth();
    public Transform Transform { get; }
}