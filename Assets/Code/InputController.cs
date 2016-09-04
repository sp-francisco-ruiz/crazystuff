using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Managers;

public class InputController : MonoBehaviour 
{
    [SerializeField]
    Button MoveLeftButton;
    [SerializeField]
    Button MoveRightButton;

    void OnEnable()
    {
        MoveRightButton.onClick.AddListener(OnMoveRight);
        MoveLeftButton.onClick.AddListener(OnMoveLeft);
    }

    void OnDisable()
    {
        MoveRightButton.onClick.RemoveListener(OnMoveRight);
        MoveLeftButton.onClick.RemoveListener(OnMoveLeft);
    }

    void OnMoveRight()
    {
        EventDispatcher.Instance.Raise<GameEvents.OnMoveRightEvent>();
    }

    void OnMoveLeft()
    {
        EventDispatcher.Instance.Raise<GameEvents.OnMoveLeftEvent>();
    }

}
