using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverScreen : UIScreen
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Button _continueButton;

    [Inject] private SignalBus _signalBus;
    [Inject] private GameManager _gameManager;

    protected override void Init()
    {
        _signalBus.Subscribe<WaveFinishSignal>(OnWaveFinish);
        _continueButton.onClick.AsObservable().Subscribe(_ => OnContinue()).AddTo(this);
    }

    private void OnContinue()
    {
        _gameManager.StarPrepare();
    }

    private void OnWaveFinish(WaveFinishSignal signal)
    {
        _resultText.text = signal.IsDone ? "Level complete" : "Failed";
        _buttonText.text = signal.IsDone ? "Continue" : "Replay";
    }
}