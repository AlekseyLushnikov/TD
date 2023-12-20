using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIScreen> _screens;
    private UIScreen _current;
    [Inject] private GameManager _gameManager;

    private void Start()
    {
        _gameManager.GameModel.GameState.Subscribe(ShowScreen).AddTo(this);
    }

    public void ShowScreen(GameState type)
    {
        if (_current != null)
        {
            _current.Hide();
        }

        _current = _screens.FirstOrDefault(x => x.Type == type);
        _current.Show();
    }
}