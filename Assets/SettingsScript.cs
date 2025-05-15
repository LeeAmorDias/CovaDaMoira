using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSettings;
    public void IncreaseVolume()
    {
        if(playerSettings.volume != 10)
            playerSettings.volume += 1;
    }
    public void IncreaseBrightness()
    {
        if(playerSettings.brightness != 10)
            playerSettings.brightness += 1;
    }
    public void DecreaseVolume()
    {
        if(playerSettings.volume != 0)
            playerSettings.volume -= 1;
    }
    public void DecreaseBrightness()
    {
        if(playerSettings.brightness != 0)
            playerSettings.brightness -= 1;
    }
}
