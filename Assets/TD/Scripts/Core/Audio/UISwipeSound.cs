using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(HorizontalScrollSnap))]
public class UISwipeSound : MonoBehaviour
{
    private HorizontalScrollSnap _scrollSnap;

    private void Start()
    {
        _scrollSnap = GetComponent<HorizontalScrollSnap>();
        _scrollSnap.OnSelectionChangeStartEvent.AsObservable()
            .Subscribe(_ => AudioManager.Instance.PlayShoot(SoundType.Swipe)).AddTo(this);
    }
}