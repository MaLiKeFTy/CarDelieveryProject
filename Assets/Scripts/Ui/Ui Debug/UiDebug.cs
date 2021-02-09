using TMPro;
using UnityEngine;

public class UiDebug : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugTxt;

    void Awake()
    {
        FloatingUiController.TouchedEvent += (value) => debugTxt.text = value;
    }

}
