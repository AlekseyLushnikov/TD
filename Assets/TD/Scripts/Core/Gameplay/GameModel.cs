using UniRx;

public class GameModel
{
    public ReactiveProperty<GameState> GameState { get; private set; } = new ();
    
    private LevelsData _data => LevelsData.Data;
    public Level CurrentLevel => _data.Levels[CurrentLevelId];

    public int CurrentLevelId { get; set; } = 0;
    public Wave CurrentWave => _data.Levels[CurrentLevelId].Waves[CurrentWaveId];

    public int CurrentWaveId { get; set; } = 0;

    public bool TryNextWave()
    {
        if (CurrentWaveId + 1 < _data.Levels[CurrentLevelId].Waves.Count)
        {
            CurrentWaveId++;
            return true;
        }
        else
        {
            if (CurrentLevelId + 1 < _data.Levels.Count)
            {
                CurrentLevelId++;
                CurrentWaveId = 0;
            }

            return false;
        }
    }
}