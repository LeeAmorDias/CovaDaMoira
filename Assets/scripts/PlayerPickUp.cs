using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private Transform CamTransform; 
    [SerializeField]
    private LayerMask pickUpLayer; 
    [SerializeField]
    private float pickUpDistance = 50f;

    private ObjectsPick objectsPick;
    private bool withOutline;
    // Update is called once per frame
    void Update()
    {
        Vector3 size2 = Vector3.one;
        size2 = Vector3.one * 2f;
        if (Physics.Raycast(CamTransform.position, CamTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayer))
        {
            objectsPick = raycastHit.transform.GetComponent<ObjectsPick>();
            if(withOutline == false && objectsPick != null){
                objectsPick.AddOutline();
                withOutline = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                    objectsPick = raycastHit.transform.GetComponent<ObjectsPick>();
                    if (objectsPick != null)
                    {
                        objectsPick.pickedUp();
                    }
            }
        }else{
            if(withOutline == true && objectsPick != null){
                objectsPick.RemoveOutline();
                withOutline = false;
            }
        }


        
    }
}
