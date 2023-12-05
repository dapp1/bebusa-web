using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomization : MonoBehaviour
{
  [SerializeField] private List<PlayerCustomizationData> faceData = new List<PlayerCustomizationData>();
  [SerializeField] private List<PlayerCustomizationData> hairData = new List<PlayerCustomizationData>();
  [SerializeField] private List<PlayerCustomizationData> beardMustacheData = new List<PlayerCustomizationData>();
  [SerializeField] private List<PlayerCustomizationData> torsoData = new List<PlayerCustomizationData>();
  [SerializeField] private List<PlayerCustomizationData> legsData = new List<PlayerCustomizationData>();
  [SerializeField] private List<PlayerCustomizationData> accessoriesData = new List<PlayerCustomizationData>();
  [SerializeField] private Button maleButton;
  [SerializeField] private Button femaleButton;
  [SerializeField] private GameObject facePanel;
  [SerializeField] private GameObject hairPanel;
  [SerializeField] private GameObject torsoPanel;
  [SerializeField] private GameObject legsPanel;
  [SerializeField] private GameObject accessoriesPanel;
  [SerializeField] private GameObject beardMustachePanel;
  [SerializeField] private GameObject headwear;
  [SerializeField] private GameObject face;
  [SerializeField] private GameObject hair;
  [SerializeField] private GameObject beardMustache;
  [SerializeField] private GameObject torso;
  [SerializeField] private GameObject legs;
  [SerializeField] private GameObject accessories;
  public ColorPanel hairColorPanel;
  public ColorPanel beardMustacheColorPanel;
  public ColorPanel torsoColorPanel;
  public ColorPanel legsColorPanel;
  public ColorPanel accessoriesColorPanel;

  private Dictionary<string, GameObject> bodyParts = new Dictionary<string, GameObject>();
  private Dictionary<string, GameObject> currentItems = new Dictionary<string, GameObject>();

  private void Start()
  {
    bodyParts.Add("Face", face);
    bodyParts.Add("Hair", hair);
    bodyParts.Add("BeardMustache", beardMustache);
    bodyParts.Add("Torso", torso);
    bodyParts.Add("Legs", legs);
    bodyParts.Add("Accessories", accessories);

    // Отключаем все панели цветов
    hairColorPanel.gameObject.SetActive(false);
    beardMustacheColorPanel.gameObject.SetActive(false);
    torsoColorPanel.gameObject.SetActive(false);
    legsColorPanel.gameObject.SetActive(false);
    accessoriesColorPanel.gameObject.SetActive(false);

    DeleteAllFromCustomization();
    DisplayCustomization();
  }

  public void MaleCustomization()
  {
    maleButton.interactable = false;
    femaleButton.interactable = true;
  }

  public void FemaleCustomization()
  {
    femaleButton.interactable = false;
    maleButton.interactable = true;
  }

  private void DisplayCustomization()
  {
    DisplayCustomizationPart(faceData, facePanel, null);
    DisplayCustomizationPart(hairData, hairPanel, hairColorPanel);
    DisplayCustomizationPart(beardMustacheData, beardMustachePanel, beardMustacheColorPanel);
    DisplayCustomizationPart(torsoData, torsoPanel, torsoColorPanel);
    DisplayCustomizationPart(legsData, legsPanel, legsColorPanel);
    DisplayCustomizationPart(accessoriesData, accessoriesPanel, accessoriesColorPanel);
  }

  private void DisplayCustomizationPart(List<PlayerCustomizationData> dataList, GameObject panel, ColorPanel colorPanel)
  {
    foreach (PlayerCustomizationData data in dataList)
    {
      PlayerCustomizationData currentData = data;
      // GameObject itemButton = Instantiate(data.itemButtonPrefub, panel.transform);
      // itemButton.GetComponentsInChildren<Image>()[1].sprite = data.itemSprite;

      // if (bodyParts.ContainsKey(data.itemName))
      // {
      //     itemButton.GetComponent<Button>().onClick.RemoveAllListeners();
      //     itemButton.GetComponent<Button>().onClick.AddListener(() =>
      //     {
      //         if (currentItems.ContainsKey(currentData.itemName) && currentItems[currentData.itemName] != null)
      //         {
      //             Destroy(currentItems[currentData.itemName]);
      //         }

      //         currentItems[currentData.itemName] = Instantiate(currentData.itemPrefab, bodyParts[currentData.itemName].transform);

      //         if (colorPanel != null)
      //         {
      //             colorPanel.gameObject.SetActive(true);
      //             SetColorsInPanel(currentData.itemColor, currentData.itemColor1, currentData.itemColor2, colorPanel);
      //             colorPanel.color1.GetComponent<Button>().onClick.RemoveAllListeners();
      //             colorPanel.color1.GetComponent<Button>().onClick.AddListener(() =>
      //             {
      //                 SetPrefab(currentData.itemPrefab, currentData.itemName);
      //             });

      //             colorPanel.color2.GetComponent<Button>().onClick.RemoveAllListeners();
      //             colorPanel.color2.GetComponent<Button>().onClick.AddListener(() =>
      //             {
      //                 SetPrefab(currentData.itemPrefab1, currentData.itemName);
      //             });

      //             colorPanel.color3.GetComponent<Button>().onClick.RemoveAllListeners();
      //             colorPanel.color3.GetComponent<Button>().onClick.AddListener(() =>
      //             {
      //                 SetPrefab(currentData.itemPrefab2, currentData.itemName);
      //             });
      //         }
      //     });
      // }
      // else
      // {
      //     Debug.LogError("Body part not found: " + data.itemName);
      // }
    }
  }

  private void SetPrefab(GameObject itemPrefab, string itemName)
  {
    if (currentItems.ContainsKey(itemName) && currentItems[itemName] != null)
    {
      Destroy(currentItems[itemName]);
    }

    currentItems[itemName] = Instantiate(itemPrefab, bodyParts[itemName].transform);
  }

  private void SetColorsInPanel(Color color1, Color color2, Color color3, ColorPanel colorPanel)
  {
    // Скрываем все панели
    hairColorPanel.gameObject.SetActive(false);
    beardMustacheColorPanel.gameObject.SetActive(false);
    torsoColorPanel.gameObject.SetActive(false);
    legsColorPanel.gameObject.SetActive(false);
    accessoriesColorPanel.gameObject.SetActive(false);

    // Показываем нужную панель
    // colorPanel.gameObject.SetActive(true);
    // colorPanel.color1.GetComponent<Image>().color = color1;
    // colorPanel.color2.GetComponent<Image>().color = color2;
    // colorPanel.color3.GetComponent<Image>().color = color3;
  }


  private void DeleteAllFromCustomization()
  {
    foreach (Transform child in facePanel.transform)
    {
      Destroy(child.gameObject);
    }
    foreach (Transform child in hairPanel.transform)
    {
      Destroy(child.gameObject);
    }
    foreach (Transform child in beardMustachePanel.transform)
    {
      Destroy(child.gameObject);
    }
    foreach (Transform child in torsoPanel.transform)
    {
      Destroy(child.gameObject);
    }
    foreach (Transform child in legsPanel.transform)
    {
      Destroy(child.gameObject);
    }
    foreach (Transform child in accessoriesPanel.transform)
    {
      Destroy(child.gameObject);
    }
  }
}
