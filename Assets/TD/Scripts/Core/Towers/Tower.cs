using System;
using UniRx;
using UnityEngine;
using Zenject;

public class Tower : MonoBehaviour, IAttackCapable, IPoolable<TowerSettings, IMemoryPool>
{
    [SerializeField] private float _cooldown;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private TowerTargetFounder _targetFounder;
    private Transform _bulletSpawnPoint;

    private IDisposable _disposable;
    private IMemoryPool _pool;

    private TowerView _view;
    public TowerSettings Settings { get; private set; }
    
    public Action OnAttack { get; set; }
    public IDamageable AttackTarget { get; private set; }

    private void Start()
    {
        _targetFounder.Target.Subscribe(SetAttackTarget).AddTo(this);
    }

    private void Update()
    {
        if (AttackTarget != null)
        {
            _view.Turret.LookAt(AttackTarget.Transform, Vector3.up);
        }
    }
    
    public void StartAttack()
    {
        _disposable = Observable.Timer(TimeSpan.FromSeconds(_cooldown)).Repeat().Subscribe(_ => Attack()).AddTo(this);
    }

    public void StopAttack()
    {
        _disposable?.Dispose();
    }


    public void SetAttackTarget(IDamageable target)
    {
        AttackTarget = target;
        if (AttackTarget == null)
        {
            _disposable?.Dispose();
        }
        else
        {
            StartAttack();
        }
    }

    public void Attack()
    {
        if (AttackTarget == null) return;

        var bullet = Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.identity);
        bullet.Launch(AttackTarget.Transform.position);
        OnAttack?.Invoke();
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(TowerSettings settings, IMemoryPool pool)
    {
        _pool = pool;
        Settings = settings;
        if (_view == null)
        {
            _view = Instantiate(settings.TowerView, transform);
        }

        _bulletSpawnPoint = _view.BulletSpawnPoint;
    }

    public void Despawn()
    {
        _pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<TowerSettings, Tower>
    {
    }
}