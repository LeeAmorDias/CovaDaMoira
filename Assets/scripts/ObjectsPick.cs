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

    private Material[] originalMaterials;

    private void Awake(){
        originalMaterials = meshRenderer.materials;
    }
    
    public void pickedUp(){
        gameInfo.IncreaseItemsPicked();
        Destroy(this.gameObject);
    }
    public void RemoveOutline(){
        meshRenderer.materials = originalMaterials;
    }
    public void AddOutline(){
        // Add outline by swapping to the outline material
        Material[] newMaterials = new Material[meshRenderer.materials.Length + 1];

        // Copy the original materials
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            newMaterials[i] = meshRenderer.materials[i];
        }

        // Set the last material to the outline material
        newMaterials[newMaterials.Length - 1] = outlineMaterial;

        // Apply the new materials array to the MeshRenderer
        meshRenderer.materials = newMaterials;
    }

}
