using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private CellFinder _cellFinderPrefab;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<EconomicSystem>().AsSingle();
        Container.Bind<CellFinder>().FromComponentInNewPrefab(_cellFinderPrefab).AsSingle();
        Container.Bind<Map>().FromComponentInHierarchy().AsSingle();
    }
}