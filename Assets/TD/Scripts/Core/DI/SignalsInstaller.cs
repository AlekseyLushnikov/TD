using Zenject;

public class SignalsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<WaveFinishSignal>();
        Container.DeclareSignal<EnemyEliminatedSignal>();
    }
}