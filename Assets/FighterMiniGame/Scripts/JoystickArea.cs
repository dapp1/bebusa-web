using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickArea : MonoBehaviour
{
    public RectTransform joystick;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         
            if (RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition))
            {
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, null, out localPoint);

                float x = Mathf.Clamp(localPoint.x, -(transform as RectTransform).rect.width / 2, (transform as RectTransform).rect.width / 2);
                float y = Mathf.Clamp(localPoint.y, -(transform as RectTransform).rect.height / 2, (transform as RectTransform).rect.height / 2);

                joystick.localPosition = new Vector2(x, y);
            }
        }
    }
}