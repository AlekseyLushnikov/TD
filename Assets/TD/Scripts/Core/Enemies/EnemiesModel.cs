using UniRx;

public class EnemiesModel
{
    public ReactiveProperty<int> EnemiesEliminated { get; private set; } = new();
}