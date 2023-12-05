using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
  public Image image;
  public Button button;

  public void SetListeners(System.Action action)
  {
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() => action());
  }
}
