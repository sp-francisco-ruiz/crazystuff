using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Managers;

public class InputController 
{
    static InputController _instance;
    public static InputController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new InputController();
            }
            return _instance;
        }
    }

    public Vector3 Axis = Vector3.zero;

    Vector2 _beginPos;
    const float kSwipeThreshold = 5.0f;

    private InputController()
    {
    }

    void OnMoveRight()
    {
        EventDispatcher.Instance.Raise<GameEvents.OnMoveRightEvent>();
    }

    void OnMoveLeft()
    {
        EventDispatcher.Instance.Raise<GameEvents.OnMoveLeftEvent>();
    }

    void OnMoveUp()
    {
        EventDispatcher.Instance.Raise<GameEvents.OnMoveUpEvent>();
    }

    void OnMoveDown()
    {
        EventDispatcher.Instance.Raise<GameEvents.OnMoveDownEvent>();
    }

    public void Update()
    {
        Vector2 endPos = Vector3.zero;
        Vector2 direction = Vector3.zero;

        Axis = Input.acceleration;
        Debug.Log(Axis);
        #if UNITY_EDITOR
         if(Input.GetMouseButtonDown(0))
         {
            _beginPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
         }
         else if(Input.GetMouseButtonUp(0))
         {
             endPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
             direction = new Vector3(endPos.x - _beginPos.x, endPos.y - _beginPos.y);
             _beginPos = Vector3.zero;
         }
        #else
         if(Input.touches.Length > 0)
         {
             Touch t = Input.GetTouch(0);
             if(t.phase == TouchPhase.Began)
             {
                 _beginPos = new Vector2(t.position.x,t.position.y);
             }
             else if(t.phase == TouchPhase.Ended)
             {
                 endPos = new Vector2(t.position.x,t.position.y);
                 direction = new Vector3(endPos.x - _beginPos.x, endPos.y - _beginPos.y);
                 _beginPos = Vector3.zero;
             }
         }
        #endif

        if(direction.sqrMagnitude < kSwipeThreshold * kSwipeThreshold)
        {
            return;
        }
         direction.Normalize();

        if(direction.y > 0 && direction.x > -0.5f && direction.x < 0.5f)
         {
             Debug.Log("up swipe");
             OnMoveUp();
         }

        if(direction.y < 0 && direction.x > -0.5f && direction.x < 0.5f)
         {
             Debug.Log("down swipe");
             OnMoveDown();
         }

        if(direction.x < 0 && direction.y > -0.5f && direction.y < 0.5f)
         {
             Debug.Log("left swipe");
            OnMoveLeft();
         }

        if(direction.x > 0 && direction.y > -0.5f && direction.y < 0.5f)
         {
            Debug.Log("right swipe");
            OnMoveRight();
         }
    }
}
