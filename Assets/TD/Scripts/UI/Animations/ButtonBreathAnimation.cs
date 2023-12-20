using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class ButtonBreathAnimation : MonoBehaviour
{
    private IDisposable _disposable;
    private RectTransform Rect => transform as RectTransform;
    private float _animationTime = 1f;
    private float _breathPercent = 0.1f;
    private Vector2 _size;

    private void Awake()
    {
        _size = Rect.sizeDelta;
    }

    private void OnEnable()
    {
        _disposable = Observable.FromCoroutine(Animate).Repeat().Subscribe();
    }

    private void OnDisable()
    {
        _disposable?.Dispose();
    }

    private IEnumerator Animate()
    {
        var time = 0f;
        
        while (time < _animationTime)
        {
            time += Time.deltaTime;
            Rect.sizeDelta = _size * Mathf.Lerp(1f, 1f + _breathPercent, time / _animationTime);
            yield return null;
        }

        time = 0f;
        while (time < _animationTime)
        {
            time += Time.deltaTime;
            Rect.sizeDelta = _size * Mathf.Lerp(1f + _breathPercent,1f, time / _animationTime);
            yield return null;
        }
    }
}