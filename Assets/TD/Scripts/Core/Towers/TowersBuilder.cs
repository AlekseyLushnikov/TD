using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TowersBuilder : MonoBehaviour
{
    [Inject] private CellFinder _cellFinder;
    [Inject] private TowersModel _towersModel;
    [Inject] private EconomicSystem _economicSystem;
    [Inject] private Tower.Factory _factory;

    private void Start()
    {
        _towersModel.CurrentId.Subscribe(OnTowerSelect).AddTo(this);
    }

    private void OnTowerSelect(int id)
    {

    }

    private void Update()
    {
        if (_cellFinder.CurrentCell == null) return;
        var currentCell = _cellFinder.CurrentCell;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currentCell.State == CellState.Busy)
                {
                    var tower = currentCell.Tower;
                    if (tower != null)
                    {
                        _economicSystem.AddCoins(tower.Settings.Cost);
                        tower.Despawn();
                    }
                    currentCell.SetState(CellState.Opened);
                }
            }
            else if (currentCell.State == CellState.Opened)
            {
                var settings = TowersData.GetData(_towersModel.CurrentId.Value);
                if (settings == null) return;
                
                if (_economicSystem.TrySpendCoins(settings.Cost))
                {
                    var tower = _factory.Create(settings);
                    tower.transform.position = currentCell.transform.position;
                    currentCell.SetState(CellState.Busy, tower);
                }
            }
        }
    }
}