using System.Collections.Generic;
using UnityEngine;
using static Customization;

public class StreetCustomization : MonoBehaviour
{
    [SerializeField] private PlayerCustomize playerCustomize;
    [SerializeField] private List<Data> data = new List<Data>();

    private void Start()
    {
        SetSkin(data);
    }

    public void SetSkin(List<Data> dataList)
    {
        if (dataList == null)
        {
            Debug.LogError("Data list is null");
            return;
        }
        string gender = PlayerPrefs.GetInt("IsMale") == 1 ? "Male" : "Female";

        // Enumerate over each BodyPart
        foreach (BodyPart bodyPart in System.Enum.GetValues(typeof(BodyPart)))
        {
            // Retrieve the saved item ID and color index for this body part
            string itemKey = $"{gender}_{bodyPart}";
            string colorKey = $"{gender}_{bodyPart}_Color";

            if (PlayerPrefs.HasKey(itemKey))
            {
                int itemId = PlayerPrefs.GetInt(itemKey);
                int colorIndex = PlayerPrefs.HasKey(colorKey) ? PlayerPrefs.GetInt(colorKey) : 0;

                // Retrieve the saved item and color preset for this body part
                Data data = dataList.Find(x => x.bodyPart == bodyPart);
                PlayerCustomizationData item = data.items.Find(x => x.itemId == itemId);
                if (item.materials[colorIndex] == null)
                {
                    colorIndex = 0;
                }
                MaterialsObject colorPreset = item.materials[colorIndex];


                // Apply the saved item and color preset to this body part
                playerCustomize.SetBodyPart(bodyPart, itemId);
                playerCustomize.SetMaterials(bodyPart, itemId, colorPreset.materials);
            }
        }

        // Retrieve and apply the saved skin material
        if (PlayerPrefs.HasKey("SkinID"))
        {
            int skinID = PlayerPrefs.GetInt("SkinID");
            Material savedSkin = GetMaterialById(skinID);
            if (savedSkin != null)
            {
                playerCustomize.SetSkin(savedSkin);
            }
        }
    }

    private Material GetMaterialById(int materialID)
    {
        Material[] allMaterials = Resources.FindObjectsOfTypeAll<Material>();
        foreach (Material material in allMaterials)
        {
            if (material.GetInstanceID() == materialID)
            {
                return material;
            }
        }
        return null;
    }
}
