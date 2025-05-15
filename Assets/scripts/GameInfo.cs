using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class GameInfo : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private AudioMixer mainMixer;
    [SerializeField]
    private PostProcessVolume PostProcessVolume;

    private ColorGrading colorGrading;

    private float volume;
    private int sens; 
    private float brightness; 

    private int itemsPicked;
    private int branchesHit;

    public int ItemsPicked => itemsPicked;
    public int BranchesHit => branchesHit;
    public int Sens => sens;



    void Awake()
    {
        PostProcessVolume.profile.TryGetSettings(out colorGrading);
        UpdateSettings();     
    }

    public void UpdateSettings()
    {
        volume = playerSettings.volume;
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume / 10) * 20);

        sens = playerSettings.sens;

        brightness = playerSettings.brightness;

        colorGrading.gamma.value = new Vector4(0f, 0f, 0f, ((brightness + 20) /100)); // Only w is used
        colorGrading.gamma.overrideState = true;
    }

    public void IncreaseItemsPicked()
    {
        itemsPicked++;
    }
    public void IncreaseBranchesHit(){
        branchesHit++;
    }
}
