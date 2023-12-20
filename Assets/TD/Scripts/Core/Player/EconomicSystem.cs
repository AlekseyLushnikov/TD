using UniRx;
using Zenject;

public class EconomicSystem : IInitializable
{
    private readonly SignalBus _signalBus;
    public ReactiveProperty<int> Coins { get; private set; }
    
    public EconomicSystem(SignalBus signalBus)
    {
        _signalBus = signalBus;
        Coins = new ReactiveProperty<int>(2000);
    }

    public void Initialize()
    {
        _signalBus.Subscribe<EnemyEliminatedSignal>(OnEnemyEliminated);
    }

    private void OnEnemyEliminated()
    {
        //TODO take from settings
        Coins.Value += 20;
    }

    public bool TrySpendCoins(int coins)
    {
        if (Coins.Value - coins >= 0)
        {
            Coins.Value -= coins;
            return true;
        }

        return false;
    }

    public void AddCoins(int coins)
    {
        Coins.Value += coins;
    }
}