using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TowerPreviewController : MonoBehaviour
{
    [SerializeField] private LayerMask _layerToPlace;
    private TowerPreview _currentPreview;

    [Inject] private CellFinder _cellFinder;
    [Inject] private TowersModel _towersModel;
    [Inject] private TowerPreview.Factory _factory;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _towersModel.CurrentId.Subscribe(GetPreview).AddTo(this);
    }

    private void GetPreview(int id)
    {
        if (_currentPreview != null)
        {
            _currentPreview.Despawn();
            _currentPreview = null;
        }
        
        if (id == -1) return;

        _currentPreview = _factory.Create();
    }

    private void Update()
    {
        if (_currentPreview == null) return;

        if (_cellFinder.CurrentCell != null)
        {
            if (_cellFinder.CurrentCell.State == CellState.Opened)
            {
                _currentPreview.transform.position = _cellFinder.CurrentCell.PlacePoint.position;
                _currentPreview.gameObject.SetActive(true);
            }

            return;
        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 100f, _layerToPlace))
        {
            if (!_currentPreview.gameObject.activeInHierarchy)
            {
                _currentPreview.gameObject.SetActive(true);
            }
            
            _currentPreview.transform.position = hit.point;
        }
        else
        {
            _currentPreview.gameObject.SetActive(false);
        }
    }
}