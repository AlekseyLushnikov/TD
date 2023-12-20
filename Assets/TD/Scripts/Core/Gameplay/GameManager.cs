using UniRx;
using UnityEngine;
using Zenject;

public class GameManager : IInitializable
{
    public GameModel GameModel { get; } = new ();
    private readonly SignalBus _signalBus;

    public GameManager(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnWaveFinish(WaveFinishSignal signal)
    {
        if (signal.IsDone)
        {
            var isNextWave = GameModel.TryNextWave();
            GameModel.GameState.Value = isNextWave ? GameState.GamePrepare : GameState.GameOver;
        }
        else
        {
            GameModel.GameState.Value = GameState.GameOver;
        }
    }

    public void StarPrepare()
    {
        GameModel.GameState.Value = GameState.GamePrepare;
    }

    public void StartGame()
    {
        GameModel.GameState.Value = GameState.Game;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<WaveFinishSignal>(OnWaveFinish);
    }
    
    public void SetCurrentLevel(int id)
    {
        GameModel.CurrentLevelId = id;
    }
}
