using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Transform _container;
    [SerializeField] private Enemy _enemyPrefab;
    public override void InstallBindings()
    {
        Container.BindFactory<EnemySettings, Map, Enemy, Enemy.Factory>()
            .FromPoolableMemoryPool<EnemySettings, Map, Enemy, EnemyPool>(poolBinder => poolBinder
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransform(_container));
        
        Container.Bind<EnemiesManager>().AsSingle();
    }
    
    class EnemyPool : MonoPoolableMemoryPool<EnemySettings, Map, IMemoryPool, Enemy>
    {
    }
}