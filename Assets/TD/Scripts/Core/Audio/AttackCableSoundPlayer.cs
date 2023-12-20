using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class AttackCableSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField, SerializeInterface(typeof(IAttackCapable))]
    private Object _attackCable;

    private IAttackCapable AttackCable => _attackCable as IAttackCapable;

    private void Start()
    {
        AttackCable.OnAttack += OnAttackPlaySound;
    }

    private void OnAttackPlaySound()
    {
        _soundsSource.PlayOneShot(_clip);
    }
}