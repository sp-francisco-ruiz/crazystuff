using UnityEngine;

namespace Game.Managers
{
    public class TimeManager : MonoBehaviour
    {
        static TimeManager _instance;
        public static TimeManager Instance
        {
            get
            {
                if(_instance == null)
                    Object.FindObjectOfType<TimeManager>();
                return _instance;
            }

            private set
            {
                if(_instance != null)
                    throw new UnityException("There can be only one TimeManager");
                _instance = value;
            }
        }

        bool _paused;
        public static bool paused
        {
            get
            {
                return Instance != null ? Instance._paused : false;
            }
            set
            {
                if(Instance != null)
                    Instance._paused = value;
            }
        }

        float _timeScale = 1f;
        public static float timeScale
        {
            get
            {
                return Instance != null ? Instance._timeScale : 0f;
            }
            set
            {
                if(Instance != null)
                    Instance._timeScale = value;
            }
        }

        float _time;
        public static float time
        {
            get
            {
                return Instance != null ? Instance._time : 0f;
            }
            set
            {
                if(Instance != null)
                    Instance._time = value;
            }
        }

        float _deltaTime;
        public static float deltaTime
        {
            get
            {
                return Instance != null ? Instance._deltaTime : 0f;
            }
            set
            {
                if(Instance != null)
                    Instance._deltaTime = value;
            }
        }

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if(paused)
                return;

            _time += Time.time * timeScale;
            _deltaTime = Time.deltaTime * _timeScale;
        }
    }
}