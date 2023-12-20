using UnityEngine;
using Zenject;

public class TowerPreview : MonoBehaviour, IPoolable<IMemoryPool>
{
    private IMemoryPool _pool;
    public class Factory : PlaceholderFactory<TowerPreview>
    {
        
    }
    
    public void Despawn()
    {
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
    }
}