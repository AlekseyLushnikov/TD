using System;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private string _walkForwardAnimation = "walk_forward";
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.Play(_walkForwardAnimation);
    }
}