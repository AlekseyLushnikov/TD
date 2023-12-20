using UniRx;

public interface ITargetFinder<T>
{
    public ReactiveProperty<T> Target { get;}

    void TryFindTarget();
    void CheckTargetOutOfVision();
}