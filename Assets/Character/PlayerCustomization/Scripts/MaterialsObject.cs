using UnityEngine;

[CreateAssetMenu(fileName = "MaterialItem", menuName = "Customization/Material", order = 0)]
public class MaterialsObject : ScriptableObject
{
    [SerializeField] public int id;
    [SerializeField] public Color previewColor;
    [SerializeField] public Material[] materials;
}
