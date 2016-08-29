using UnityEngine;
using Game.Managers;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour 
    {
        public float speed = 10f;

    	void Start () 
        {

    	}

    	void Update () 
        {
    	    transform.position = transform.position + transform.forward * speed * TimeManager.deltaTime;
    	}


        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("platformend"))
            {
                EventDispatcher.Instance.Raise(new GameEvents.ExitPlatformEvent(other.GetComponent<PlatformController>()));
            }
        }
    }
}