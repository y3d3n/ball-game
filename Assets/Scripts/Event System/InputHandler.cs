using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float maxScreenTouchTime = 0.3f;
    float currentScreenTouchedTime = 0f;

    bool touchInitiated = false;

    InputValues inputValues;
    Vector3 touchStartPos,touchEndPos;



    private void Awake()
    {
        inputValues = new InputValues();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentScreenTouchedTime = maxScreenTouchTime;
            touchInitiated = true;
            touchStartPos = Input.mousePosition;

        }
        
        if (Input.GetMouseButtonUp(0))
        {

            touchInitiated = false;

            if (currentScreenTouchedTime > 0)
            {
                OnScreenTapped();
                return;

            }
            CalculateSwipeState();

        }
        if (touchInitiated)
        {
            //             if (Input.mousePosition != touchStartPos)
            //             {
            //                 currentScreenTouchedTime = 0;
            // 
            //             }

            Vector3 mousepos = Input.mousePosition;
            if (Vector2.Distance(mousepos,touchStartPos)>50)
            {
                currentScreenTouchedTime = 0;
            }
            currentScreenTouchedTime -= Time.deltaTime;
            if (currentScreenTouchedTime <=0)
            {
                
                OnScreenSwiped();

            }
        }

    }

    private void CalculateSwipeState()
    {
        if (Mathf.Abs(Input.mousePosition.x - touchStartPos.x) > Mathf.Abs(Input.mousePosition.y - touchStartPos.y))
        {

            //is a horizontal swipe
            if (Input.mousePosition.x > touchStartPos.x)
            {
                //right swipe
                inputValues.swipeState = SwipeState.ESWIPERIGHT;
            }
            else
            {
                // left swipe
                inputValues.swipeState = SwipeState.ESWIPELEFT;
            }
        }
        else
        {
            // is a vertical swipe
            if (Input.mousePosition.y > touchStartPos.y)
            {
                //up swipe
                inputValues.swipeState = SwipeState.ESWIPEUP;
            }
            else
            {
                // down swipe
                inputValues.swipeState = SwipeState.ESWIPEDOWN;
            }
        }
    }

    void OnScreenSwiped()
    {
        if (EventSystem.Instance == null)
        {
           
            return;
        }

        

        inputValues.setInputValues(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), false, true);
        EventSystem.Instance.FireEvent(inputValues);
        
    }

    void OnScreenTapped()
    {
        if (EventSystem.Instance == null)
        {
            return;
        }
        inputValues.setInputValues(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), true, false);
        EventSystem.Instance.FireEvent(inputValues);
    }
}

public class InputValues:EventInfo
{
    
     public float swipeX, swipeY;
     public bool  tapped;
     public bool isSwiping;
     public bool swipeLeft, swipeRight, swipedown, swipeUp;
     public SwipeState swipeState;

    public void setInputValues(float swipeX, float swipeY, bool tapped, bool isSwiping)
    {
        this.swipeX = swipeX;
        this.swipeY = swipeY;
        this.tapped = tapped;
        this.isSwiping = isSwiping;
    }

   
}

