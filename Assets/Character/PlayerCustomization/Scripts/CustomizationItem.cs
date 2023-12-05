using UnityEngine;

public class CustomizationItem : MonoBehaviour
{
    public int itemId;
    public MeshFilter meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public int skinId;
    public bool isSkin;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetMaterials(Material[] materials)
    {
        if (meshRenderer != null)
        {
            meshRenderer.GetComponent<MeshRenderer>().materials = materials;
        }

        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.materials = materials;
        }
    }

    public void SetSkin(Material material, int id)
    {
        if (meshRenderer != null && isSkin)
        {
            Material[] materials = meshRenderer.GetComponent<MeshRenderer>().materials;
            materials[id] = material;
            SetMaterials(materials);
        }

        if (skinnedMeshRenderer != null && isSkin)
        {
            Material[] materials = skinnedMeshRenderer.materials;
            materials[id] = material;
            SetMaterials(materials);
        }
    }
}
