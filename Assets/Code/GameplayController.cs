using UnityEngine;
using Game.Managers;
using System.Collections.Generic;

namespace Game.Controllers
{
    public class GameplayController : MonoBehaviour 
    {
        public GameObject platformPrefab;

        static GameplayController _instance;
        public static GameplayController Instance
        {
            get
            {
                if(_instance == null)
                    Object.FindObjectOfType<GameplayController>();
                return _instance;
            }

            private set
            {
                if(_instance != null)
                    throw new UnityException("There can be only one GameplayController");
                _instance = value;
            }
        }

        List<PlatformController> _Platforms = new List<PlatformController>();

    	void Awake () 
        {
    	    Instance = this;
            DontDestroyOnLoad(gameObject);
            for(int i = 0; i < 10; ++i)
            {
                CreatePlatform();
            }
    	}

        void OnEnable()
        {
            EventDispatcher.Instance.RemoveListener<GameEvents.ExitPlatformEvent>(OnExitPlatform);
            EventDispatcher.Instance.AddListener<GameEvents.ExitPlatformEvent>(OnExitPlatform);
        }

    	void OnDisable () 
        {
            EventDispatcher.Instance.RemoveListener<GameEvents.ExitPlatformEvent>(OnExitPlatform);
    	}

        public void OnExitPlatform(GameEvents.ExitPlatformEvent e)
        {
            Destroy(_Platforms[0].gameObject, 1f);
            _Platforms.RemoveAt(0);
            CreatePlatform();
        }

        void CreatePlatform()
        {
            var go = Instantiate(platformPrefab);
            var controller = go.GetComponent<PlatformController>();
            if(_Platforms.Count < 1)
            {
                controller.transform.position = Vector3.zero;
                controller.transform.rotation = Quaternion.identity;
            }
            else
            {
                controller.transform.position = _Platforms[_Platforms.Count - 1].end.transform.position;
                controller.transform.LookAt(go.transform.position + _Platforms[_Platforms.Count - 1].end.transform.forward);
                controller.SetObstacles();
            }
            _Platforms.Add(controller);
        }
    }
}