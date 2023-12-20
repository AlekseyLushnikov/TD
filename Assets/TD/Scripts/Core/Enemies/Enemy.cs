using UniRx;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IDamageable, IPoolable<EnemySettings, Map, IMemoryPool>
{
    [SerializeField] private EnemyTargetFinder _enemyTargetFinder;
    [SerializeField] private EnemyAttackCapable _attackCapable;
    [SerializeField] private EnemyAgent _agent;

    private EnemyView _view;

    private IMemoryPool _pool;
    private readonly CompositeDisposable _disposable = new();

    public IReactiveProperty<int> Health { get; private set; }
    public IReadOnlyReactiveProperty<bool> IsEliminated { get; private set; }


    public void SetDamage(int damage)
    {
        Health.Value -= damage;
    }

    public void OnZeroHealth()
    {
        _pool.Despawn(this);
    }

    public Transform Transform => transform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Bullet>(out var bullet))
        {
            SetDamage(bullet.Damage);
        }
    }

    public class Factory : PlaceholderFactory<EnemySettings, Map, Enemy>
    {
    }

    public void OnDespawned()
    {
        _pool = null;
        _disposable.Clear();
    }

    public void OnSpawned(EnemySettings settings, Map map, IMemoryPool pool)
    {
        _pool = pool;
        Health = new ReactiveProperty<int>(settings.StartingHealth);
        IsEliminated = Health.Select(x => x <= 0).ToReactiveProperty();
        IsEliminated.Where(x => x).Subscribe(_ => OnZeroHealth()).AddTo(_disposable);
        _enemyTargetFinder.Target.Subscribe(OnTargetFound).AddTo(_disposable);
        _agent.Init(map);

        //TODO check id and replace if not equal
        if (_view == null)
        {
            SpawnView(settings.EnemyViewPrefab);
        }
    }

    private void SpawnView(EnemyView prefab)
    {
        _view = Instantiate(prefab, transform);
    }

    public void OnTargetFound(Wall target)
    {
        if (target == null) return;

        _attackCapable.SetAttackTarget(target);
        _agent.StopWalk();
    }
}