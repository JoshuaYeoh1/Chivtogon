using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    Vector2 startTouchPos, currentPos, endTouchPos;
    bool stopTouch;
    public float swipeRange=50, tapRange=10;

    void Update()
    {
        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began)
        {
            startTouchPos=Input.GetTouch(0).position;
        }

        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Moved)
        {
            currentPos = Input.GetTouch(0).position;

            Vector2 distance = currentPos - startTouchPos;

            if(!stopTouch)
            {
                if(distance.x < -swipeRange)
                {
                    swipeLeft();
                    stopTouch=true;
                }
                else if(distance.x > swipeRange)
                {
                    swipeRight();
                    stopTouch=true;
                }
                else if(distance.y > swipeRange)
                {
                    swipeUp();
                    stopTouch=true;
                }
                else if(distance.y < -swipeRange)
                {
                    swipeDown();
                    stopTouch=true;
                }
            }
        }

        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended)
        {
            stopTouch=false;

            endTouchPos = Input.GetTouch(0).position;

            Vector2 distance = endTouchPos - startTouchPos;

            if(Mathf.Abs(distance.x)<tapRange && Mathf.Abs(distance.y)< tapRange)
            {
                tap();
            }
        }
    }

    void swipeLeft()
    {
        Debug.Log("swiped left");

        player.turn.turnleft();
    }

    void swipeRight()
    {
        Debug.Log("swiped right");

        player.turn.turnright();
    }

    void swipeUp()
    {
        Debug.Log("swiped up");

        player.parry.parry();
    }

    void swipeDown()
    {
        Debug.Log("swiped down");

        player.attack.attack();
    }

    void tap()
    {
        Debug.Log("tapped");
    }
}






// void Update()
//     {
//         if(Singleton.instance.controlsEnabled)
//         {
//             if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began)
//             {
//                 startTouchPos=Input.GetTouch(0).position;
//             }

//             if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended)
//             {
//                 endTouchPos=Input.GetTouch(0).position;

//                 Vector2 inputVector = endTouchPos-startTouchPos;

//                 if(Mathf.Abs(inputVector.x)>Mathf.Abs(inputVector.y))
//                 {
//                     if(inputVector.x<0)
//                     {
//                         swipeLeft();
//                     }
//                     else
//                     {
//                         swipeRight();
//                     }
//                 }
//                 else
//                 {
//                     if(inputVector.y>0)
//                     {
//                         swipeUp();
//                     }
//                     else
//                     {
//                         swipeDown();
//                     }
//                 }
//             }
//         }
//     }









// public delegate void StartTouchEvent(Vector2 position, float time);
    // public event StartTouchEvent onStartTouch;
    // public delegate void EndTouchEvent(Vector2 position, float time);
    // public event EndTouchEvent onEndTouch;

    // TouchControls touch;

    // void Awake()
    // {
    //     touch = new TouchControls();
    // }

    // void OnEnable()
    // {
    //     touch.Enable();
    // }

    // void OnDisable()
    // {
    //     touch.Disable();
    // }

    // void Start()
    // {
    //     touch.Touch.TouchPress.started+=ctx=>StartTouch(ctx);
    //     touch.Touch.TouchPress.canceled+=ctx=>EndTouch(ctx);
    // }

    // void StartTouch(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Touch started "+touch.Touch.TouchPosition.ReadValue<Vector2>());

    //     if(onStartTouch!=null) onStartTouch(touch.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    // }
    // void EndTouch(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Touch ended");

    //     if(onEndTouch!=null) onEndTouch(touch.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    // }
