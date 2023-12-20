using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class HealthView : MonoBehaviour
{
    [SerializeField, SerializeInterface(typeof(IDamageable))]
    private Object _damageable;

    [SerializeField] private RectTransform _bar;
    [SerializeField] private float _hideTime = 5f;

    public IDamageable Damageable => _damageable as IDamageable;
    private int _startHealth;
    private float _sizeX;

    private IDisposable _disposable;

    private void Start()
    {
        _startHealth = Damageable.Health.Value;
        _sizeX = _bar.sizeDelta.x;
        Damageable.Health.Subscribe(OnHealthChanged).AddTo(this);
        _bar.gameObject.SetActive(false);
    }

    private void OnHealthChanged(int health)
    {
        _disposable?.Dispose();
        _bar.sizeDelta = new Vector2(health * _sizeX / _startHealth, _bar.sizeDelta.y);
        _bar.gameObject.SetActive(true);

        _disposable = Observable.Timer(TimeSpan.FromSeconds(_hideTime)).Subscribe(_ => _bar.gameObject.SetActive(false))
            .AddTo(this);
    }
}