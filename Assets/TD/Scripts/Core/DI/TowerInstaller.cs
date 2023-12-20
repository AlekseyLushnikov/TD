using UnityEngine;
using Zenject;

public class TowerInstaller : MonoInstaller
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TowerPreview _towerPreview;
    [SerializeField] private Transform _towersContainer;
    [SerializeField] private Transform _previewContainer;
    
    public override void InstallBindings()
    {
        Container.Bind<TowersModel>().FromComponentInHierarchy().AsSingle();
        
        Container.BindFactory<TowerSettings, Tower, Tower.Factory>()
            .FromPoolableMemoryPool<TowerSettings, Tower, TowerPool>(poolBinder => poolBinder
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_tower)
                .UnderTransform(_towersContainer));
        
        Container.BindFactory<TowerPreview, TowerPreview.Factory>()
            .FromPoolableMemoryPool<TowerPreview, TowerPreviewPool>(poolBinder => poolBinder
                .WithInitialSize(1)
                .FromComponentInNewPrefab(_towerPreview)
                .UnderTransform(_previewContainer));
    }
    
    class TowerPool : MonoPoolableMemoryPool<TowerSettings, IMemoryPool, Tower>
    {
    }
    
    class TowerPreviewPool : MonoPoolableMemoryPool<IMemoryPool, TowerPreview>
    {
    }
}