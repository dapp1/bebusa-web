using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Customization : MonoBehaviour
{
    public static Customization Instance { get; private set; }

    [SerializeField] private List<Data> maleData = new List<Data>();
    [SerializeField] private List<Data> femaleData = new List<Data>();

    [SerializeField] private List<Panel> panels = new List<Panel>();
    [SerializeField] private PlayerCustomize malePlayerCustomize;
    [SerializeField] private PlayerCustomize femalePlayerCustomize;

    [SerializeField] private ItemCustomizationButton itemButtonPrefub;

    [SerializeField] private Material skinMaterial;
    [SerializeField] private Button maleButton;
    [SerializeField] private Button femaleButton;
    [SerializeField] private GameObject beardMustachePanel;
    [SerializeField] private GameObject male;
    [SerializeField] private GameObject female;


    private bool isMale = true;

    public void Male()
    {
        isMale = true;
        maleButton.interactable = false;
        femaleButton.interactable = true;
        beardMustachePanel.SetActive(true);
        male.gameObject.SetActive(true);
        female.gameObject.SetActive(false);
        LoadCustomization(maleData, "Male");
        LoadSavedSkin();
        Display(maleData);
        LoadCustomization(maleData, "Male");
        PlayerPrefs.SetInt("Skin", 10);
        PlayerPrefs.SetInt("IsMale", 1);

    }

    public void Female()
    {
        isMale = false;
        femaleButton.interactable = false;
        maleButton.interactable = true;
        beardMustachePanel.SetActive(false);
        female.gameObject.SetActive(true);
        male.gameObject.SetActive(false);
        LoadCustomization(femaleData, "Female");
        LoadSavedSkin();
        Display(femaleData);
        PlayerPrefs.SetInt("IsMale", 0);
        PlayerPrefs.SetInt("Skin", 11);
        if (PlayerPrefs.HasKey("FemaleFirst"))
        {
            LoadCustomization(femaleData, "Female");
        }
        else
        {

            PlayerPrefs.SetInt("FemaleFirst", 1);
            SetDefault(femaleData);
        }





    }
    public void SelectSkin()
    {
        if (PlayerPrefs.GetInt("IsMale") == 1)
        {
            PlayerPrefs.SetInt("Skin", 10);
            SceneManager.LoadScene(0);
        }
        else
        {
            PlayerPrefs.SetInt("Skin", 11);
            SceneManager.LoadScene(0);
        }

    }

    public void SetSkin(Material material)
    {
        if (isMale)
        {
            skinMaterial = material;
            malePlayerCustomize.SetSkin(skinMaterial);
            SaveSkin(skinMaterial);
        }
        else if (!isMale)
        {
            skinMaterial = material;
            femalePlayerCustomize.SetSkin(skinMaterial);
            SaveSkin(skinMaterial);
        }
    }

    private void SaveSkin(Material material)
    {
        int materialID = material.GetInstanceID();
        PlayerPrefs.SetInt("SkinID", materialID);
        PlayerPrefs.Save();
    }

    public void Display(List<Data> dataList)
    {
        Clear();

        foreach (var data in dataList)
        {
            foreach (var item in data.items)
            {
                Panel panel = panels.Find(x => x.bodyPart == data.bodyPart);

                ItemCustomizationButton itemButton = Instantiate(itemButtonPrefub, panel.content);
                itemButton.image.sprite = item.itemSprite;

                foreach (PlayerCustomizationData curItem in currentItems)
                {
                    if (itemButton.isBlocked == false)
                        itemButton.SetBlock(curItem.blockedItems.Contains(item.itemId));

                    if (curItem.itemId == item.itemId)
                    {
                        itemButton.Current();
                        Debug.Log("curItem.itemId == item.itemId");
                        DisplayPanel();
                    }
                }


                itemButton.button.onClick.AddListener(() =>
                {
                    SetBodyPart(data.bodyPart, item.itemId, item.materials[0]);
                    DisplayPanel();
                    Display(dataList);
                });

                void DisplayPanel()
                {
                    if (panel.colorPanel != null)
                    {
                        panel.colorPanel.DisplayColors(data.bodyPart, item.itemId, item.materials);
                    }
                }
            }
        }
    }

    public void SetBodyPart(BodyPart bodyPart, int itemId, MaterialsObject colorPreset)
    {
        string itemKey = isMale ? $"Male_{bodyPart}" : $"Female_{bodyPart}";
        string colorKey = isMale ? $"Male_{bodyPart}_Color" : $"Female_{bodyPart}_Color";

        if (isMale)
        {
            malePlayerCustomize.SetBodyPart(bodyPart, itemId);
            malePlayerCustomize.SetMaterials(bodyPart, itemId, colorPreset.materials);
            malePlayerCustomize.SetSkin(skinMaterial);
        }
        else if (!isMale)
        {
            femalePlayerCustomize.SetBodyPart(bodyPart, itemId);
            femalePlayerCustomize.SetMaterials(bodyPart, itemId, colorPreset.materials);
            femalePlayerCustomize.SetSkin(skinMaterial);
        }

        PlayerPrefs.SetInt(itemKey, itemId);

        Data data = isMale ? maleData.Find(x => x.bodyPart == bodyPart) : femaleData.Find(x => x.bodyPart == bodyPart);
        PlayerCustomizationData item = data.items.Find(x => x.itemId == itemId);
        int colorIndex = Array.IndexOf(item.materials, colorPreset);

        PlayerPrefs.SetInt(colorKey, colorIndex);
        PlayerPrefs.Save();

        PlayerCustomizationData i = currentItems.Find(x => x.bodyPart == bodyPart);

        if (i != null)
        {
            currentItems.Remove(i);
        }

        currentItems.Add(item);
    }

    private void Start()
    {


        Instance = this;
        if (PlayerPrefs.HasKey("IsMale"))
        {
            if (PlayerPrefs.GetInt("IsMale") == 1)
            {
                isMale = true;
                Male();
                LoadCustomization(maleData, "Male");
                LoadSavedSkin();
                Display(maleData);
            }
            else
            {
                isMale = false;
                Female();
                LoadCustomization(femaleData, "Female");
                LoadSavedSkin();
                Display(femaleData);
            }
        }
        else
        {
            if (isMale)
            {
                PlayerPrefs.SetInt("isMale", 1);
                Display(maleData);
                if (!PlayerPrefs.HasKey("MaleFirst"))
                {
                    PlayerPrefs.SetInt("MaleFirst", 1);
                    SetDefault(maleData);
                }
                Male();
            }
            else if (!isMale)
            {
                Display(femaleData);
                if (!PlayerPrefs.HasKey("FemaleFirst"))
                {
                    PlayerPrefs.SetInt("FemaleFirst", 1);
                    SetDefault(femaleData);
                }
                Female();
            }
            else
            {
                Male();
                Display(maleData);
                if (!PlayerPrefs.HasKey("MaleFirst"))
                {
                    PlayerPrefs.SetInt("MaleFirst", 1);
                    SetDefault(maleData);
                }
            }

            PlayerPrefs.SetInt("IsMale", isMale ? 1 : 0);
            PlayerPrefs.Save();
        }



    }

    public void LoadSavedSkin()
    {
        if (PlayerPrefs.HasKey("SkinID"))
        {
            int skinID = PlayerPrefs.GetInt("SkinID");
            Material savedSkin = GetMaterialById(skinID);
            if (savedSkin != null)
            {
                skinMaterial = savedSkin;
                if (isMale)
                {
                    malePlayerCustomize.SetSkin(skinMaterial);
                }
                else
                {
                    femalePlayerCustomize.SetSkin(skinMaterial);
                }
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

    private List<PlayerCustomizationData> currentItems = new List<PlayerCustomizationData>();
    private void LoadCustomization(List<Data> dataList, string gender)
    {
        foreach (var data in dataList)
        {
            var itemKey = $"{gender}_{data.bodyPart}";
            var colorKey = $"{gender}_{data.bodyPart}_Color";
            if (PlayerPrefs.HasKey(itemKey))
            {
                int savedItemId = PlayerPrefs.GetInt(itemKey);
                PlayerCustomizationData item = data.items.Find(x => x.itemId == savedItemId);
                if (item != null)
                {
                    int colorIndex = PlayerPrefs.HasKey(colorKey) ? PlayerPrefs.GetInt(colorKey) : 0;
                    MaterialsObject colorPreset = item.materials[colorIndex];
                    SetBodyPart(data.bodyPart, savedItemId, colorPreset);
                }
            }
        }
    }



    private void SetDefault(List<Data> data)
    {
        Debug.Log("Setting default");
        foreach (var dataItem in data)
        {
            if (dataItem.items.Count > 0)
            {
                var firstItem = dataItem.items[0];

                SetBodyPart(dataItem.bodyPart, firstItem.itemId, firstItem.materials[0]);
            }
        }
    }




    private void Clear()
    {
        foreach (var panel in panels)
        {
            panel.Clear();
        }
    }

    public enum BodyPart
    {
        Face,
        Hair,
        BeardMustache,
        Torso,
        Legs,
        Accessories
    }

    [Serializable]
    public class Data
    {
        public BodyPart bodyPart;
        public List<PlayerCustomizationData> items;
    }

    [Serializable]
    public class Panel
    {
        public BodyPart bodyPart;
        public Transform content;
        public ColorPanel colorPanel;

        public void Clear()
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
