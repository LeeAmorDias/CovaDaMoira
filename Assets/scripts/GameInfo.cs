using UnityEngine;

public class GameInfo : MonoBehaviour
{
    private int itemsPicked;

    private int branchesHit;

    public int ItemsPicked => itemsPicked;

    public int BranchesHit => branchesHit;

    public void IncreaseItemsPicked(){
        itemsPicked++;
    }
    public void IncreaseBranchesHit(){
        branchesHit++;
    }
}
