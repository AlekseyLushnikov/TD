using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Zenject;

public class MainMenu : UIScreen
{
    [SerializeField] private Button _startGame;
    [SerializeField] private HorizontalScrollSnap _scrollSnap;
    [SerializeField] private LevelCard _cardPrefab;
    [Inject] private GameManager _gameManager;
    
    private void Start()
    {
        _startGame.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(this);
        foreach (var level in LevelsData.Data.Levels)
        {
            var card = Instantiate(_cardPrefab, _scrollSnap._screensContainer);
            card.Init(level.LevelName);
            _scrollSnap.AddChild(card.gameObject);
        }

        _scrollSnap.OnSelectionPageChangedEvent.AddListener(OnSelectionChanged);
    }

    private void OnSelectionChanged(int id)
    {
        _gameManager.SetCurrentLevel(id);
    }

    private void StartGame()
    {
        _gameManager.StarPrepare();
    }
}