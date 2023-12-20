using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class TowersSelectorView : MonoBehaviour
{
    [SerializeField] private TowerButtonView _viewPrefab;
    [SerializeField] private Transform _container;
    [Inject] private TowersModel _towersModel;

    [Inject] private EconomicSystem _economicSystem;

    private readonly List<TowerButtonView> _views = new();

    private void Start()
    {
        CreateOptions();
    }

    private void CreateOptions()
    {
        foreach (var tower in TowersData.Data.TowersSettings)
        {
            var view = Instantiate(_viewPrefab, _container);
            view.Init(tower.TowerName, tower.Cost, tower.Id);
            view.OnTowerSelect += Select;
            view.CheckForTowerAvailable(_economicSystem.Coins.Value);
            _views.Add(view);
        }

        _economicSystem.Coins.Subscribe(OnCoinsChanged).AddTo(this);
    }

    private void OnCoinsChanged(int coins)
    {
        foreach (var view in _views)
        {
            view.CheckForTowerAvailable(coins);
        }
    }

    public void Select(int value)
    {
        _towersModel.Select(value);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Select(-1);
        }
    }
}