using System.Collections.Generic;
using UnityEngine;

namespace Level
{

    public class GroundManager : MonoBehaviour
    {
        public Vector3 endPoint;
        public ObjectPooler pooler;
        private List<GameObject> _activeGroundGO; 
        void FixedUpdate()
        {
            
            _moveGround();

        }

        private void _moveGround(){

            foreach (GameObject ground in _activeGroundGO)
            {
                
                

            }

        }


    }

}
