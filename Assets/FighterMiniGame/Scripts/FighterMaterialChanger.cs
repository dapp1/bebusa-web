using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMaterialChanger : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    [SerializeField] private Texture matCapTexture;

    private void ChangeMaterialsShaders()
    {
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {
            foreach (Material material in skinnedMeshRenderer.materials)
            {
                material.shader = Shader.Find("Custom/MatCapSimple");
                material.SetTexture("_MatCap", matCapTexture);
            }
        }
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            foreach (Material material in meshRenderer.materials)
            {
                material.shader = Shader.Find("Custom/MatCapSimple");
                material.SetTexture("_MatCap", matCapTexture);

            }
        }

    }
    private void Start()
    {
        ChangeMaterialsShaders();
    }
}
