using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BgCrop : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().Crop();
    }
}