using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private Transform CamTransform; 
    [SerializeField]
    private LayerMask pickUpLayer; 

    private ObjectsPick objectsPick;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickUpDistance = 2f;

            if (Physics.Raycast(CamTransform.position, CamTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayer))
            {

                ObjectsPick pickedObject = raycastHit.transform.GetComponent<ObjectsPick>();

                if (pickedObject != null)
                {
                    pickedObject.pickedUp();
                }
            }
        }
        
    }
}
