using UnityEngine;

public class ObjectsPick : MonoBehaviour
{
    [SerializeField]
    private GameInfo gameInfo;
    
    public void pickedUp(){
        gameInfo.IncreaseItemsPicked();
        Destroy(this.gameObject);
    }

}
