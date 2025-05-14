using UnityEngine;

public class PlayerHitBranch : MonoBehaviour
{
    [SerializeField]
    private float ableToMakeSound = 30f;
    [SerializeField]
    private GameInfo gameInfo;
    private float Timer;

    [SerializeField]
    private AudioClip somDoGalho;
    [SerializeField]
    private AudioSource audioSource;

    void Awake()
    {
        Timer = ableToMakeSound;
    }
    void Update(){
        Timer += Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && Timer > ableToMakeSound)
        {
            Timer = 0;
            gameInfo.IncreaseBranchesHit();
            if (!audioSource.isPlaying)
            {
                audioSource.clip = somDoGalho;
                audioSource.Play();
            }
        }
    }

}
