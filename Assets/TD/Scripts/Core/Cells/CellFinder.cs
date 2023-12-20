using System;
using UniRx;
using UnityEngine;
using Zenject;

public class CellFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _cellMask;

    private Ray _ray;
    private RaycastHit _hit;

    private Cell _currentCell;
    private Camera _camera;

    private IDisposable _disposable;
    
    public Cell CurrentCell => _currentCell;
    
    [Inject] private GameManager _gameManager;

    private void Start()
    {
        _camera = Camera.main;
        _gameManager.GameModel.GameState.Subscribe(OnStateChanged).AddTo(this);
    }

    private void OnStateChanged(GameState gameState)
    {
        if (gameState is GameState.GamePrepare or GameState.Game)
        {
            StartCellFinding();
        }
        else
        {
            StopCellFinding();
        }
    }

    private void StartCellFinding()
    {
        _disposable?.Dispose();
        _disposable = Observable.EveryUpdate().Subscribe(_=>TryFindCell()).AddTo(this);
    }

    public void StopCellFinding()
    {
        _disposable?.Dispose();
    }

    private void TryFindCell()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _cellMask))
        {
            _currentCell = _hit.transform.GetComponent<Cell>();
        }
        else
        {
            _currentCell = null;
        }
    }
}