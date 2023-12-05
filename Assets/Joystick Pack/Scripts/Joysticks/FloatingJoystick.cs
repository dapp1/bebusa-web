using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    public static bool IsBeingUsed { get; private set; }
    public static bool firstTouch;

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Swipe.firstTouch)
            firstTouch = false;
        else
            firstTouch = true;

        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        IsBeingUsed = true;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        firstTouch = false;
        background.gameObject.SetActive(false);
        IsBeingUsed = false;
        base.OnPointerUp(eventData);
    }
}