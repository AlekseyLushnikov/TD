using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerButtonView : MonoBehaviour, IDragHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _towerName;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Image _icon;

    private int _cost;
    
    private RectTransform _rect => transform as RectTransform;

    private int _id;
    public Action<int> OnTowerSelect;

    private void Start()
    {
        _button.onClick.AsObservable().Subscribe(_ => OnTowerSelect?.Invoke(_id)).AddTo(this);
    }

    public void CheckForTowerAvailable(int coins)
    {
        _button.interactable = _cost <= coins;
    }

    public void Init(string towerName, int cost, int id)
    {
        _towerName.text = towerName;
        _costText.text = cost.ToString();
        _cost = cost;
        _id = id;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(_rect, eventData.position))
        {
            OnTowerSelect?.Invoke(_id);
        }
    }
}