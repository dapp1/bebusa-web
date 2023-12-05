using System;
using System.Collections.Generic;
using UnityEngine;
using static Customization;

public class PlayerCustomize : MonoBehaviour
{
    [SerializeField] private List<Part> parts = new List<Part>();



    public void SetBodyPart(BodyPart bodyPart, int itemId)
    {
        Part part = parts.Find(x => x.bodyPart == bodyPart);

        if (part == null)
        {
            Debug.LogError("Part not found");
            return;
        }

        part.SetActiveItems(false);

        try
        {
            CustomizationItem item = part.GetItem(itemId);

            if (item == null)
            {
                Debug.LogError($"Item with id {itemId} not found");
                return;
            }

            item.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void SetMaterials(BodyPart bodyPart, int itemId, Material[] materials)
    {
        if (parts == null)
        {
            return;
        }

        Part part = parts.Find(x => x.bodyPart == bodyPart);

        if (part == null)
        {
            return;
        }

        CustomizationItem item = part.GetItem(itemId);

        if (item == null)
        {
            return;
        }

        item.SetMaterials(materials);
    }


    public void SetSkin(Material skin)
    {
        foreach (var part in parts)
        {
            foreach (var item in part.items)
            {
                
                item.SetSkin(skin, item.skinId);
            }
        }
    }

    [Serializable]
    public class Part
    {
        public BodyPart bodyPart;
        public List<CustomizationItem> items = new List<CustomizationItem>();

        public CustomizationItem GetItem(int itemId)
        {
            return items.Find(x => x.itemId == itemId);
        }

        public void SetActiveItems(bool active)
        {
            foreach (var item in items)
            {
                item.SetActive(active);
            }
        }
    }
}
