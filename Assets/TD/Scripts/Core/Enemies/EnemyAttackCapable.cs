using System;
using UniRx;using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackCapable : MonoBehaviour, IAttackCapable
{
    [SerializeField] private Bullet _bulletPrefab;

    private IDisposable _disposable;
    public Action OnAttack { get; set; }

    public IDamageable AttackTarget { get; private set; }

    public void SetAttackTarget(IDamageable target)
    {
        AttackTarget = target;
        StartAttack();
    }

    public void Attack()
    {
        if (AttackTarget == null) return;
        
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.Launch(AttackTarget.Transform.position);
        OnAttack?.Invoke();
    }
    
    public void StartAttack()
    {
        _disposable = Observable.Timer(TimeSpan.FromSeconds(5f)).Repeat().Subscribe(_ => Attack());
    }

    public void StopAttack()
    {
        _disposable?.Dispose();
    }
}