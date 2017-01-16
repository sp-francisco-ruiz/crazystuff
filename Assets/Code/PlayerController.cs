using UnityEngine;
using Game.Managers;
using System.Collections;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour 
    {
        public static float PlayerSpeed = 0f;
        public static float PlayerTurnSpeed = 5f;
        public static float PlayerSlideSpeed = 20f;

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

        Vector3 _targetPos = Vector3.zero;

        public Animation _animation;

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
            _targetPos = Vector3.zero;
            _targetPos.x = InputController.Instance.Axis.x * Time.deltaTime * PlayerSlideSpeed;

            transform.position = transform.position + transform.forward * PlayerSpeed * Time.deltaTime;
            PlayerObj.transform.localPosition += _targetPos;

            _targetPos.x = PlayerObj.transform.localPosition.x;
            _targetPos.x = Mathf.Clamp(PlayerObj.transform.localPosition.x, LeftObj.transform.localPosition.x, RightObj.transform.localPosition.x);
            PlayerObj.transform.localPosition = _targetPos;

            PlayerSpeed += Time.deltaTime * PlayerSpeedMultiplier;
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
            //_animation.SetTrigger("left");
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
            //_animation.SetTrigger("right");
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("platformend"))
            {
                EventDispatcher.Instance.Raise(new GameEvents.ExitPlatformEvent());
            }
            else if(other.CompareTag("turn"))
            {
                StartCoroutine(Turn(other.transform.forward));
            }
            else if(other.CompareTag("obstacle"))
            {
                Debug.Log("I Die");
            }
        }

        IEnumerator Turn(Vector3 target)
        {
            while(Vector3.Dot(transform.forward, target) < 1f)
            {
                transform.forward = Vector3.Lerp(transform.forward, target, Time.deltaTime * PlayerTurnSpeed);
                yield return null;
            }
        }
    }
}