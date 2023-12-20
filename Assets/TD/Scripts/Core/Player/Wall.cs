using System;
using UniRx;
using UnityEngine;
using Zenject;

public class Wall : MonoBehaviour, IDamageable
{
    [Inject] private SignalBus _signalBus;
    private void Awake()
    {
        //TODO to settings
        Health = new ReactiveProperty<int>(100);
        IsEliminated = Health.Select(x => x <= 0).ToReactiveProperty();
        IsEliminated.Where(x => x).Subscribe(_ => OnZeroHealth()).AddTo(this);
    }

    public void SetDamage(int damage)
    {
        Health.Value -= damage;
    }

    public IReactiveProperty<int> Health { get; private set; }
    
    public IReadOnlyReactiveProperty<bool> IsEliminated { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Bullet>(out var bullet))
        {
            SetDamage(bullet.Damage);
        }
    }

    public void OnZeroHealth()
    {
        _signalBus.Fire(new WaveFinishSignal(false));
        Destroy(gameObject);
    }

    public Transform Transform => transform;
}