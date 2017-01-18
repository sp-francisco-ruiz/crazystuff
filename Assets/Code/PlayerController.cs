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

        Vector3 _targetPos = Vector3.zero;

        Animation _animation;
        Rigidbody _rigidBody;
        Transform _transform;

        void Awake()
        {
            _animation = PlayerObj.GetComponent<Animation>();
            _rigidBody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        void Update () 
        {
            _targetPos = _transform.position + _transform.forward * Time.deltaTime * PlayerSpeed;
            _targetPos.x += InputController.Instance.Axis.x * Time.deltaTime * PlayerSlideSpeed;

            _rigidBody.MovePosition(_targetPos);

            PlayerSpeed += Time.deltaTime * PlayerSpeedMultiplier;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("platform"))
            {
                PlatformController platform = other.GetComponent<PlatformController>();
                if(platform != null)
                {
                    platform.PlayerEntered();
                }
            }
            else if(other.CompareTag("platformend"))
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
                _transform.forward = Vector3.Lerp(transform.forward, target, Time.deltaTime * PlayerTurnSpeed);
                yield return null;
            }
        }
    }
}