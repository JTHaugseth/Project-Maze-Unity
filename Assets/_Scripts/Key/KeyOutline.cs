using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOutline : MonoBehaviour
{
    public Material outlineMaterial;

    // Renderer-reference to shuffle 2 materials.
    private Renderer rend;
    // Array to store the original material
    private Material[] originalMaterials;

    void Start()
    {
        rend = GetComponent<Renderer>();
        // Storing the original material when the game starts.
        originalMaterials = rend.materials;
    }

    public void ShowOutline()
    {
        // Creates a new material-array with an extra slot for the new outline-material.
        Material[] newMaterials = new Material[originalMaterials.Length + 1];
        // Transfer the original material to the new array.
        originalMaterials.CopyTo(newMaterials, 0);
        // Adding the outline material to the array.
        newMaterials[newMaterials.Length - 1] = outlineMaterial;
        // Applying the new array to the renderer. 
        rend.materials = newMaterials;
    }

    public void HideOutline()
    {
        // Restoring the original array when the outline-material should be hidden. 
        rend.materials = originalMaterials;
    }
}
