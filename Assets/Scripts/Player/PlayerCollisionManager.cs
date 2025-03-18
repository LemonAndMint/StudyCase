using System;
using UnityEngine;

namespace Player
{
    
    public class PlayerCollisionManager : MonoBehaviour
    {
        public delegate void CollisionEventHandler();
        /// <summary>
        /// Critical is something that can change the play state of our player. Right now we only have GameOver.
        /// </summary>
        public event CollisionEventHandler OnCriticalCollision;
        private bool _contacted;
        public bool Contacted { get => _contacted; }

        public string CriticalContactLayerName;
        private int _criticalLayerName;

        private void Awake() {
            
            if(!string.IsNullOrEmpty(CriticalContactLayerName)){

                _criticalLayerName = LayerMask.NameToLayer(CriticalContactLayerName);
                return;

            }

            Debug.LogError("No Critical Layer Name provided");

        }

        void OnCollisionEnter(Collision collision)
        {
            
            _contacted = true;

            if(collision.gameObject.layer == _criticalLayerName){

                OnCriticalCollision?.Invoke();

            }

        }

        void OnCollisionExit(Collision collision)
        {
            
            _contacted = false;

        }
    }

}

