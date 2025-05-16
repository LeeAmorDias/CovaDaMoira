using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public int volume;
    public int sens;
    public int brightness;
    public bool win = false;
    
}
