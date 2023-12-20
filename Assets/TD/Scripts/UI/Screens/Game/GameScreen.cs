using Zenject;

public class GameScreen : UIScreen
{
    [Inject] private EnemiesManager _enemiesManager;

    public override void Show()
    {
        base.Show();
        _enemiesManager.StartWave();
    }
}