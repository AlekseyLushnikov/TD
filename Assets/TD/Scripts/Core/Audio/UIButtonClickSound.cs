using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonClickSound : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AsObservable().Subscribe(_ => AudioManager.Instance.PlayShoot(SoundType.ButtonClick)).AddTo(this);
    }
}