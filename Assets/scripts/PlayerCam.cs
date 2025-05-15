using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField]
    private GameInfo gameInfo;
    [SerializeField]
    private Transform orientation;

    private float xRotation, yRotation;
    private int sens;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        sens = gameInfo.Sens;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime *  (sens * 50);
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * (sens * 50);

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,90f);

        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = Quaternion.Euler(0,yRotation,0); 

    }
}
