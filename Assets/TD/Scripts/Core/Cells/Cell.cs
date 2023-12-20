using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Cell : MonoBehaviour
{
    [SerializeField] private CellState _state;
    [SerializeField] private Transform _placePoint;
    public CellState State => _state;
    public Transform PlacePoint => _placePoint;
    public Tower Tower { get; private set; }

    public void SetState(CellState state, Tower tower = null)
    {
        _state = state;
        Tower = tower;
    }
}