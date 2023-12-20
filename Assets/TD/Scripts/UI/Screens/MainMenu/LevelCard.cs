using TMPro;
using UnityEngine;

public class LevelCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelName;

    public void Init(string levelName)
    {
        _levelName.text = levelName;
    }
}