using UnityEngine;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSet;
    [SerializeField]
    private TextMeshProUGUI volume, sens, brightness; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volume.text = playerSet.volume.ToString();
        sens.text = playerSet.sens.ToString();
        brightness.text = playerSet.brightness.ToString();
    }
}
