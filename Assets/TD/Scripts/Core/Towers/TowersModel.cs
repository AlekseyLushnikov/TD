using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class TowersModel : MonoBehaviour
{
    public ReactiveProperty<int> CurrentId { get; } = new (-1);

    public void Select(int id)
    {
        CurrentId.Value = id;
    }
}