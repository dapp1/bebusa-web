using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{

    public static bool IsBeingUsed { get; private set; }




    public override void OnPointerDown(PointerEventData eventData)
    {
        IsBeingUsed = true;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        IsBeingUsed = false;
        base.OnPointerUp(eventData);
    }

   
}
