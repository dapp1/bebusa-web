using UnityEngine;
using static Customization;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CustomItem", menuName = "Customization/Item", order = 0)]

public class PlayerCustomizationData : ScriptableObject
{
    public int itemId;
    public BodyPart bodyPart;
    public Sprite itemSprite;
    public MaterialsObject[] materials;
    public List<int> blockedItems;
}
