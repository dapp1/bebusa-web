using UnityEngine;
using static Customization;

public class ColorPanel : MonoBehaviour
{
    [SerializeField] private ColorButton colorButtonPrefub;
    private ColorButton[] colorButtons;

    public void Awake()
    {
        Clear();
    }
    public void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void DisplayColors(BodyPart part, int itemId, MaterialsObject[] materials)
    {
        Clear();
        // Debug.Log(part + " " + itemId + " " + materials.Length);
        foreach (MaterialsObject material in materials)
        {
            // Debug.Log(material.previewColor);
            ColorButton colorButton = Instantiate(colorButtonPrefub, transform);
            colorButton.image.color = material.previewColor;
            colorButton.SetListeners(() =>
            {
                Customization.Instance.SetBodyPart(part, itemId, material);
            });
        }
    }
}
