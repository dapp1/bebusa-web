using UnityEngine;

public class CustomizationObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void SetMaterial(Material material, int skinId)
    {
        meshRenderer.materials[skinId] = material;
    }

    public void SetMaterials(Material[] materials)
    {
        meshRenderer.materials = materials;
    }
}
