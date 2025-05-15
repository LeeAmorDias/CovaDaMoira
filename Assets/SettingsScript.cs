using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSettings;
    public void IncreaseVolume()
    {
        playerSettings.volume += 1;
    }
    public void IncreaseSens()
    {
        playerSettings.sens += 1;
    }
    public void IncreaseBrightness()
    {
        playerSettings.brightness += 1;
    }
    public void DecreaseVolume()
    {
        playerSettings.volume -= 1;
    }
    public void DecreaseSens()
    {
        playerSettings.sens -= 1;
    }
    public void DecreaseBrightness()
    {
        playerSettings.brightness -= 1;
    }
}
