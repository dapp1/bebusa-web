using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Moved OnMoved;
    public static event TwoTouchMoved OnTwoTouchMoved;

    private bool isMoving = false;
    private int currentTouchId;
    private int touchCount = 0;
    Touch touch;

    public static bool firstTouch;

    private void Update()
    {
        try
        {

            if (touchCount > 1 && !FloatingJoystick.IsBeingUsed)
            {
                // Debug.Log("Two touch");
                OnTwoTouchMoved?.Invoke(InputWrapper.Input.GetTouch(0), InputWrapper.Input.GetTouch(1));
                return;
            }

            if (!isMoving || touchCount < 1) return;

            OnMoved?.Invoke(touch);
        }
        catch (System.Exception) { }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (FloatingJoystick.firstTouch)
            firstTouch = false;
        else
            firstTouch = true;

        Debug.Log("OnPointerDown");
        touchCount++;
        //currentTouchId = touchCount - 1;
        touch.phase = TouchPhase.Began;
        OnMoved?.Invoke(touch);
        isMoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        firstTouch = false;
        touchCount--;       
        touch.phase = TouchPhase.Ended;
        OnMoved?.Invoke(touch);
        isMoving = false;
    }

}

public delegate void Moved(Touch touch);

public delegate void TwoTouchMoved(Touch firstTouch, Touch secondTouch);