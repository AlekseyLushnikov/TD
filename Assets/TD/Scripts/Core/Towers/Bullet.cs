using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _initialVelocity;
    [SerializeField] private int _damage;
    private Rigidbody _rigidbody;

    public int Damage => _damage;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Observable.Timer(TimeSpan.FromSeconds(10f)).Subscribe(_ => Destroy(gameObject));
    }

    public void Launch(Vector3 targetPosition)
    {
        var direction = targetPosition - transform.position;
        var yOffset = direction.y;
        direction.y = 0;
        var distance = direction.magnitude;

        var angle = Mathf.Atan((_initialVelocity * _initialVelocity - Mathf.Sqrt(_initialVelocity * _initialVelocity * _initialVelocity * _initialVelocity - Physics.gravity.y * (Physics.gravity.y * distance * distance + 2f * yOffset * _initialVelocity * _initialVelocity))) / (Physics.gravity.y * distance));

        var vxz = _initialVelocity * Mathf.Cos(angle);
        var vy = _initialVelocity * Mathf.Sin(angle);

        var velocity = new Vector3(0, vy, vxz);
        velocity = Quaternion.LookRotation(direction) * velocity;

        _rigidbody.velocity = velocity;
    }
}