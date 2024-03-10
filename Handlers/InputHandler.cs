using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class InputHandler : MonoBehaviour
{


    #region Singleton
    public static InputHandler Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region Enable and Disable
    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.TouchSimulation.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }
#endregion

    [SerializeField] private float maxHoldTimer = .5f;
    private float currentHoldTimer;
    private bool isHolding;
    private bool isMoved;
    private bool leftSideTouch; //Determine if th touch is on left of screen or right

    //For keeping track of positions
    private Vector2 touchEndedPosition;
    private Vector2 touchStartPosition;
    private Vector2 touchDeltaPosition;
    private Vector2 touchCurrentPosition;

    private Vector2 touch2EndedPosition;
    private Vector2 touch2StartPosition;
    private Vector2 touch2DeltaPosition;
    private Vector2 touch2CurrentPosition;

    //Callback Events
    public event Action OnTouchStart;
    public event Action OnTouchEnded;
    public event Action OnHold;

    //Hold and array of all touches
    EnhancedTouch.Touch[] touches;

    [SerializeField] private int targetFrameRate;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }

    private void Update(){

        if (EventSystem.current.IsPointerOverGameObject()) return; //We will not register touch if we are touching a UI component

        touches = EnhancedTouch.Touch.activeTouches.ToArray();

        if(touches.Length > 0)
        {
            if (touches.Length == 1)
            {
                if (touches[0].began)
                {
                    TouchStart(touches[0]);
                }
                else if (touches[0].isInProgress)
                {

                    //We will not do anything if we are already holding (for prevent repeatedly event invoking)
                    if (!IsHolding())
                    {
                        //Check if we are holding or not
                        currentHoldTimer += Time.deltaTime;
                        if (currentHoldTimer > maxHoldTimer)
                        {
                            //We are holding
                            isHolding = true;

                            OnHold?.Invoke(); //Ivoking the event when we hold
                        }
                    }

                    if (touches[0].phase == UnityEngine.InputSystem.TouchPhase.Moved)
                    {
                        isMoved = true;
                        touchDeltaPosition = touches[0].delta;
                    }
                    else
                    {
                        touchDeltaPosition = Vector2.zero; // Bcz camera was still moving if we donot move
                    }
                }
                else if (touches[0].ended)
                {
                    TouchEnded(touches[0]);

                    isHolding = false;
                    currentHoldTimer = 0;
                }
            }
            else if (touches.Length == 2)
            {
                if (touches[0].began || touches[1].began)
                {
                    TouchStart(touches[0], touches[1]);
                }
                else if (touches[0].inProgress && touches[1].inProgress)
                {
                    if (touches[0].phase==UnityEngine.InputSystem.TouchPhase.Moved || touches[1].phase == UnityEngine.InputSystem.TouchPhase.Moved)
                    {
                        touchCurrentPosition = touches[0].screenPosition;
                        touch2CurrentPosition = touches[1].screenPosition;

                        touchDeltaPosition = touches[0].delta;
                        touch2DeltaPosition = touches[1].delta;
                    }
                }
                else if (touches[0].ended || touches[1].ended)
                {
                    TouchEnded(touches[0], touches[1]);
                }
            }
        }
    }

    private void TouchStart(EnhancedTouch.Touch touch)
    {
        isMoved = false;
        touchStartPosition = touch.startScreenPosition;

        if (touchStartPosition.x < Screen.width / 2)
        {
            leftSideTouch = true;
        }
        else
        {
            leftSideTouch = false;
        }

        OnTouchStart?.Invoke();
    }
    private void TouchStart(EnhancedTouch.Touch touch, EnhancedTouch.Touch touch1)
    {
        touchStartPosition = touch.startScreenPosition;
        touch2StartPosition = touch1.startScreenPosition;

        OnTouchStart?.Invoke();
    }

    private void TouchEnded(EnhancedTouch.Touch touch)
    {
        touchEndedPosition = touch.screenPosition;
        OnTouchEnded?.Invoke();
    }
    private void TouchEnded(EnhancedTouch.Touch touch, EnhancedTouch.Touch touch1)
    {
        touchEndedPosition = touch.screenPosition;
        touch2EndedPosition = touch1.screenPosition;

        OnTouchEnded?.Invoke();
    }



    public Vector2 GetTouchEndPosition()
    {
        return touchEndedPosition;
    }
    public Vector2 GetTouchStartPosition()
    {
        return touchStartPosition;
    }
    public Vector2 GetTouchDeltaPosition()
    {
        return touchDeltaPosition;
    }
    public Vector2 GetTouchCurrentPosition()
    {
        return touchCurrentPosition;
    }

    public Vector2 GetTouch2EndPosition()
    {
        return touch2EndedPosition;
    }
    public Vector2 GetTouch2StartPosition()
    {
        return touch2StartPosition;
    }
    public Vector2 GetTouch2DeltaPosition()
    {
        return touch2DeltaPosition;
    }
    public Vector2 GetTouch2CurrentPosition()
    {
        return touch2CurrentPosition;
    }


    public bool IsHolding()
    {
        return isHolding;
    }
    public bool IsMoved() {
        return isMoved;
    }

    public int GetTouchCount()
    {
        return touches.Length;
    }
    public bool IsLeftSideTouch()
    {
        return leftSideTouch;
    }
}
