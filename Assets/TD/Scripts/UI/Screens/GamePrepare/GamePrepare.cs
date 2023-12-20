using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GamePrepare : UIScreen
{
    [SerializeField] private Button _startWave;
    [Inject] private GameManager _gameManager;

    private void Start()
    {
        _startWave.onClick.AsObservable().Subscribe(StartWave).AddTo(this);
    }

    private void StartWave(Unit unit)
    {
        _gameManager.StartGame();
    }
}