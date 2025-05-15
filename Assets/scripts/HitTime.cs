using Unity.VisualScripting;
using UnityEngine;

public class HitTime : MonoBehaviour
{
    [HideInInspector]
    public float timer;
    [SerializeField]
    private float ableToMakeSound;
    public float AbleToMakeSound => ableToMakeSound;

    void Update(){
        timer += Time.deltaTime;
    }
}
