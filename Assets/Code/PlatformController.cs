using UnityEngine;
using System.Collections.Generic;

namespace Game.Controllers
{
    public class PlatformController : MonoBehaviour 
    {
        public Transform EndTrans;
        public List<Collider> Colliders;

        [SerializeField] Collider _trigger;

        void Awake()
        {
            for(int i = 0; i < Colliders.Count; ++i)
            {
                Colliders[i].enabled = false;
            }
            _trigger.enabled = true;
        }

        public void PlayerEntered()
        {
            for(int i = 0; i < Colliders.Count; ++i)
            {
                Debug.Log("Patatisimas");
                Colliders[i].enabled = true;
            }
            _trigger.enabled = false;
        }

        public void PlayerLeft()
        {
            for(int i = 0; i < Colliders.Count; ++i)
            {
                Colliders[i].enabled = false;
            }
            Destroy(gameObject, 3f);
        }
    }
}