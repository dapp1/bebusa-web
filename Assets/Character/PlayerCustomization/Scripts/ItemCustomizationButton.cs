using UnityEngine;
using UnityEngine.UI;

public class ItemCustomizationButton : MonoBehaviour
{
    [SerializeField] public Button button;
    [SerializeField] public Image image;
    [SerializeField] private Image blockImage;
    [SerializeField] private Image currentImage;
    [SerializeField] private Sprite currentSprite;

    public bool isBlocked = false;

    public void SetBlock(bool isBlock)
    {
        isBlocked = isBlock;
        blockImage.gameObject.SetActive(isBlock);
        button.interactable = !isBlock;
    }
    public void Current()
    {
        currentImage.sprite = currentSprite;
    }


}
