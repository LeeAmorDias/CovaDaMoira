using UnityEngine;

public class GameInfo : MonoBehaviour
{
    private int itemsPicked;

    public int ItemsPicked => itemsPicked;

    public void IncreaseItemsPicked(){
        itemsPicked++;
    }
}
