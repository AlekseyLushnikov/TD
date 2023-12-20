using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class CoinsView : MonoBehaviour
{
    [Inject] private EconomicSystem _economicSystem;

    private TMP_Text _coins;

    private void Start()
    {
        _coins = GetComponent<TMP_Text>();
        _economicSystem.Coins.Subscribe(OnCoinsChanged).AddTo(this);
    }
    
    private void OnCoinsChanged(int coins)
    {
        _coins.text = coins.ToString();
    }

}