using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameInfo : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private AudioMixer mainMixer;
    [SerializeField]
    private PostProcessVolume PostProcessVolume;
    [SerializeField]
    private GameObject player, Enemy, intro, victory,uizinho;

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
        intro.SetActive(true);
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        intro.SetActive(false);
        player.SetActive(true);
        Enemy.SetActive(true);
        uizinho.SetActive(true);

    }

    public void UpdateSettings()
    {
        volume = playerSettings.volume;
        float db = Mathf.Lerp(-80f, 20f, volume / 10f);
        if (db > 20)
            db = 20;
        mainMixer.SetFloat("Master", db);

        sens = playerSettings.sens;

        brightness = playerSettings.brightness;

        float gammaStrength = Mathf.Lerp(0f, 1f, brightness / 10f);
        colorGrading.gamma.value = new Vector4(gammaStrength, gammaStrength, gammaStrength, gammaStrength);
        colorGrading.gamma.overrideState = true;
    }

    public void IncreaseItemsPicked()
    {
        itemsPicked++;
    }
    public void IncreaseBranchesHit()
    {
        branchesHit++;
    }

    private void Update()
    {
        UpdateSettings();
        if (itemsPicked == 5)
        {
            player.SetActive(false);
            Enemy.SetActive(false);
            uizinho.SetActive(false);
            victory.SetActive(true);
            StartCoroutine(DeactivateAfterDelay());
        }
        /*if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu"); // Load the scene
        }*/
    }
    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main Menu"); // Load the scene

    }
}
