using UnityEngine;
using Game.Managers;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour 
    {
        public static float PlayerSpeed = 0f;

        public float PlayerSpeedInitial = 10f;
        public float PlayerSpeedMultiplier = .2f;

        [SerializeField]
        GameObject PlayerObj;

        [SerializeField]
        GameObject LeftObj;

        [SerializeField]
        GameObject MidObj;

        [SerializeField]
        GameObject RightObj;

        Vector3 _targetPos;

        //-1 0 1 : left mid right
        int _side = 0;

        void OnEnable()
        {
            EventDispatcher.Instance.AddListener<GameEvents.OnMoveLeftEvent>(OnMoveLeft);
            EventDispatcher.Instance.AddListener<GameEvents.OnMoveRightEvent>(OnMoveRight);
            PlayerSpeed = PlayerSpeedInitial;
        }

        void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener<GameEvents.OnMoveLeftEvent>(OnMoveLeft);
            EventDispatcher.Instance.RemoveListener<GameEvents.OnMoveRightEvent>(OnMoveRight);
        }

    	void Update () 
        {
            transform.position = transform.position + transform.forward * PlayerSpeed * TimeManager.deltaTime;
            PlayerObj.transform.localPosition = Vector3.Lerp(PlayerObj.transform.localPosition, _targetPos, TimeManager.deltaTime * PlayerSpeed);

            PlayerSpeed += TimeManager.deltaTime * PlayerSpeedMultiplier;
    	}



        void OnMoveLeft(GameEvents.OnMoveLeftEvent e)
        {
            if(_side == 1)
            {
                _targetPos = MidObj.transform.localPosition;
                _side = 0;
            }
            else if(_side == 0)
            {
                _targetPos = LeftObj.transform.localPosition;
                _side = -1;
            }
        }

        void OnMoveRight(GameEvents.OnMoveRightEvent e)
        {
            if(_side == -1)
            {
                _targetPos = MidObj.transform.localPosition;
                _side = 0;
            }
            else if(_side == 0)
            {
                _targetPos = RightObj.transform.localPosition;
                _side = 1;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("platformend"))
            {
                EventDispatcher.Instance.Raise(new GameEvents.ExitPlatformEvent());
            }
            else if(other.CompareTag("obstacle"))
            {
                Debug.Log("I Die");
            }
        }
    }
}