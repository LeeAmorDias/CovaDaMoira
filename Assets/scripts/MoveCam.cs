using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField]
    private Transform camPos;

    private void Update()
    {
        transform.position = camPos.position;
    }
}
