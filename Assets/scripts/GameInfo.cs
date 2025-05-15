using UnityEngine;
using UnityEngine.Audio;

public class GameInfo : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private AudioMixer mainMixer;

    private int volume;
    private int sens; 
    private int brightness; 

    private int itemsPicked;
    private int branchesHit;

    public int ItemsPicked => itemsPicked;
    public int BranchesHit => branchesHit;
    public int Sens => sens;



    void Awake()
    {
        UpdateSettings();
    }

    public void UpdateSettings()
    {
        volume = playerSettings.volume;
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume/10) * 20 );

        sens = playerSettings.sens;

        
        brightness = playerSettings.brightness;   
    }

    public void IncreaseItemsPicked()
    {
        itemsPicked++;
    }
    public void IncreaseBranchesHit(){
        branchesHit++;
    }
}
