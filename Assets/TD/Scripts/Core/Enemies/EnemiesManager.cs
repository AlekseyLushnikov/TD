using System;
using UniRx;
using Zenject;

public class EnemiesManager
{
    [Inject] private GameManager _gameManager;
    
    private int _spawnedCount;
    private Map _map;
    private readonly SignalBus _signalBus;

    private Enemy.Factory _factory;
    public EnemiesModel EnemiesModel { get; }

    public EnemiesManager(Map map, Enemy.Factory factory, SignalBus signalBus)
    {
        _map = map;
        _factory = factory;
        EnemiesModel = new EnemiesModel();
        _signalBus = signalBus;
    }

    public void StartWave()
    {
        EnemiesModel.EnemiesEliminated.Value = 0;
        _spawnedCount = 0;
        var enemyCount = _gameManager.GameModel.CurrentWave.EnemyCount;
        Observable.Interval(TimeSpan.FromSeconds(1f)).TakeWhile(_ => _spawnedCount < enemyCount)
            .Subscribe(_ => Spawn());
    }

    public void Spawn()
    {
        var spawnPoint = _map.GetSpawnPoint();
        var data = EnemiesData.GetData(_gameManager.GameModel.CurrentWave.EnemyId);
        var enemy = _factory.Create(data.Settings, _map);
        enemy.transform.position = spawnPoint.transform.position;

        enemy.IsEliminated.Where(x => x).Subscribe(_ => OnEnemyEliminated());
        _spawnedCount++;
    }

    public void OnEnemyEliminated()
    {
        EnemiesModel.EnemiesEliminated.Value++;
        _signalBus.Fire(new EnemyEliminatedSignal());
        if (_gameManager.GameModel.CurrentWave.EnemyCount <= EnemiesModel.EnemiesEliminated.Value)
        {
            _signalBus.Fire(new WaveFinishSignal(true));
        }
    }
}