using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour
{
    [SerializeField] private GameState _type;
    public GameState Type => _type;
    private CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        SetVisibility(false);
        Init();
    }

    protected virtual void Init(){}

    public virtual void Show()
    {
        SetVisibility(true);
    }

    public virtual void Hide()
    {
        SetVisibility(false);
    }

    private void SetVisibility(bool visibility)
    {
        _canvasGroup.interactable = visibility;
        _canvasGroup.blocksRaycasts = visibility;
        _canvasGroup.alpha = visibility ? 1f : 0f;
        gameObject.SetActive(visibility);
    }

    protected void Back()
    {
        StopAllCoroutines();
    }
}