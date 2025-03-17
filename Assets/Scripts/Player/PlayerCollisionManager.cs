using UnityEngine;

namespace Player
{
    
    public class PlayerCollisionManager : MonoBehaviour
    {
        private bool _contacted;
        public bool Contacted { get => _contacted; }
        void OnCollisionEnter(Collision collision)
        {
            
            _contacted = true;

        }

        void OnCollisionExit(Collision collision)
        {
            
            _contacted = false;

        }
    }

}

