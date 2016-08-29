using UnityEngine;
using Game.Managers;

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

        PlatformController _lastPlatformCreated;

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
            Destroy(e.Platform.gameObject, 1f);
            CreatePlatform();
        }

        void CreatePlatform()
        {
            var go = Instantiate(platformPrefab);
            if(_lastPlatformCreated == null)
            {
                go.transform.position = Vector3.zero;
                go.transform.rotation = Quaternion.identity;
            }
            else
            {
                go.transform.position = _lastPlatformCreated.end.transform.position;
                go.transform.LookAt(go.transform.position + _lastPlatformCreated.end.transform.forward);
            }
            _lastPlatformCreated = go.GetComponent<PlatformController>();
        }
    }
}