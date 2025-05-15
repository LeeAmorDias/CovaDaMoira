using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectsPick : MonoBehaviour
{
    [SerializeField]
    private GameInfo gameInfo;
    [SerializeField]
    private Material normalMaterial;
    [SerializeField]
    private Material outlineMaterial;
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private AudioSource audioOnPick;

    private Material[] originalMaterials;

    private void Awake(){
        originalMaterials = meshRenderer.materials;
    }
    
    public void pickedUp(){
        gameInfo.IncreaseItemsPicked();
        audioOnPick.Play();
        Destroy(this.gameObject);
    }
    public void RemoveOutline(){
        meshRenderer.materials = originalMaterials;
    }
    public void AddOutline(){
        // Add outline by swapping to the outline material
        Material[] newMaterials = new Material[originalMaterials.Length + 1];

        // Copy the original materials
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            newMaterials[i] = originalMaterials[i];
        }

        // Set the last material to the outline material
        newMaterials[newMaterials.Length - 1] = outlineMaterial;

        // Apply the new materials array to the MeshRenderer
        meshRenderer.materials = newMaterials;
    }

}
