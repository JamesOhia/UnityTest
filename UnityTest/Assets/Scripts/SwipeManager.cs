using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private const float DoubleClickTime = 0.3f;

    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, doubleTap;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private float lastClickTime;

    private void Update()
    {

        tap = swipeDown = swipeUp = swipeLeft = swipeRight = doubleTap = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= DoubleClickTime)
            {
                //DoubleClick
                doubleTap = true;
            }
            else
            {
                //NormalClick
                tap = true;
            }
            isDraging = true;
            startTouch = Input.mousePosition;
            lastClickTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick <= DoubleClickTime)
                {
                    //DoubleClick
                    doubleTap = true;
                }
                else
                {
                    //NormalClick
                    tap = true;
                }
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the distance?
        if (swipeDelta.magnitude > 5)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}