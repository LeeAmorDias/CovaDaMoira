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
    private bool ispushing;
    private float timer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject Loader;
    // Update is called once per frame
    void Update()
    {
        Vector3 size2 = Vector3.one;
        size2 = Vector3.one * 2f;
        if (Physics.Raycast(CamTransform.position, CamTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayer))
        {
            objectsPick = raycastHit.transform.GetComponent<ObjectsPick>();
            if(objectsPick != null){
                objectsPick.AddOutline();
            }
            if (Input.GetKey(KeyCode.E) || Input.GetMouseButton(0))
            {
                    Loader.SetActive(true);
                    animator.SetTrigger("Load");
                    ispushing = true;
                    timer += Time.deltaTime;
                    objectsPick = raycastHit.transform.GetComponent<ObjectsPick>();
                    if (objectsPick != null&& timer > 3f)
                    {
                        objectsPick.pickedUp();
                        animator.SetTrigger("Stop Load");
                        Loader.SetActive(false);      
                    }
            }else{
                timer = 0;
                if(Loader.activeSelf)
                    animator.SetTrigger("Stop Load");
                Loader.SetActive(false);               
                ispushing = false;
            }
        }else{
            timer = 0;
            if(objectsPick != null){
                objectsPick.RemoveOutline();
            }
            if(Loader.activeSelf)
                animator.SetTrigger("Stop Load");
            Loader.SetActive(false);        
        }


        
    }
}
