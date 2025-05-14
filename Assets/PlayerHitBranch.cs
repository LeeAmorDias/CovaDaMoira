using UnityEngine;

public class PlayerHitBranch : MonoBehaviour
{
    [SerializeField]
    private GameInfo gameInfo;

    [SerializeField]
    private AudioClip somDoGalho;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource ambientSound;
    [SerializeField]
    private HitTime hitTime;
    [SerializeField]
    private Owl owl;

    void Awake()
    {
        hitTime.timer = hitTime.AbleToMakeSound;
    }
    void Update(){
        if(hitTime.timer>1.5f){
            if(!ambientSound.isPlaying)
                ambientSound.Play();
            owl.TimerCanGo = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && hitTime.timer > hitTime.AbleToMakeSound)
        {
            hitTime.timer = 0;
            gameInfo.IncreaseBranchesHit();
            if (!audioSource.isPlaying)
            {
                audioSource.clip = somDoGalho;
                audioSource.Play();
                ambientSound.Stop();
                owl.TimerCanGo = false;
            }
        }
    }

}
