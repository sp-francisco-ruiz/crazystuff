using UnityEngine;
using Game.Managers;
using System.Collections.Generic;

namespace Game.Controllers
{
    public class GameplayController : MonoBehaviour
    {
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

        public List<GameObject> PlatformPrefabs;
        public List<GameObject> TurnPlatformPrefabs;

        public List<PlatformController> _Platforms = new List<PlatformController>();


        int _tilesToTurn = 5;

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            for(int i = 0; i < 15; ++i)
            {
                CreatePlatform();
            }
        }

        void Update()
        {
            InputController.Instance.Update();
        }

        void OnEnable()
        {
            EventDispatcher.Instance.RemoveListener<GameEvents.ExitPlatformEvent>(OnExitPlatform);
            EventDispatcher.Instance.AddListener<GameEvents.ExitPlatformEvent>(OnExitPlatform);
        }

        void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener<GameEvents.ExitPlatformEvent>(OnExitPlatform);
        }

        public void OnExitPlatform(GameEvents.ExitPlatformEvent e)
        {
            Destroy(_Platforms[0].gameObject, 3f);
            _Platforms.RemoveAt(0);
            CreatePlatform();
        }

        void CreatePlatform()
        {
            GameObject go = null;
            if(Random.Range(0, 100) > 70 && _tilesToTurn < 0)
            {
                go = Instantiate(TurnPlatformPrefabs[Random.Range(0, TurnPlatformPrefabs.Count)]);
                _tilesToTurn = 5;
            }
            else
            { 
                go = Instantiate(PlatformPrefabs[Random.Range(0, PlatformPrefabs.Count)]);
                --_tilesToTurn;
            }
            var controller = go.GetComponent<PlatformController>();
            if(_Platforms.Count < 1)
            {
                controller.transform.position = Vector3.zero;
                controller.transform.rotation = Quaternion.identity;
            }
            else
            {
                controller.transform.position = _Platforms[_Platforms.Count - 1].EndTrans.position;
                controller.transform.LookAt(go.transform.position + _Platforms[_Platforms.Count - 1].EndTrans.forward);
            }
            _Platforms.Add(controller);
        }
    }
}