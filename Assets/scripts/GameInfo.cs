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
        float db = Mathf.Log10(volume / 5f) * 20f;
        if (db > 20)
            db = 20;
        mainMixer.SetFloat("Master", db );

        sens = playerSettings.sens;

        brightness = playerSettings.brightness;

        float gammaStrength = (brightness + 20f) / 50f;
        colorGrading.gamma.value = new Vector4(gammaStrength, gammaStrength, gammaStrength, gammaStrength);
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
