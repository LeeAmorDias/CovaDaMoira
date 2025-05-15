using UnityEngine;
using System.Collections;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameInfo gameInfo;
    [SerializeField]
    private TextMeshProUGUI LightsPicked;

    void Update()
    {
        LightsPicked.text = gameInfo.ItemsPicked.ToString() + "/" + "7";
    }
}
