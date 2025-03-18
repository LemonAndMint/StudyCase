using System.Collections.Generic;
using UnityEngine;

namespace Level
{

    [RequireComponent(typeof(ObjectPooler))]
    [RequireComponent(typeof(GameManager))]
    public class GroundManager : MonoBehaviour
    {
        
        public GameManager gameManager;
        [SerializeField]
        private ObjectPooler pooler;
        private List<GameObject> _activeGroundGO; 
        private float _levelSpeed;

        private void Start() {
            
            if(pooler != null)
                pooler.Init(gameManager.LevelData.planePrefbs);

            _levelSpeed = gameManager.LevelData.LevelForwardSpeed;

            _activeGroundGO = new List<GameObject>();

            _activeGroundGO.Add(pooler.GetObject());
            _activeGroundGO.Add(pooler.GetObject());
            _activeGroundGO.Add(pooler.GetObject());
            _activeGroundGO.Add(pooler.GetObject());

        }


        void FixedUpdate()
        {
            _moveGround();

        }

        public void DestroyGround(GameObject releasingGO){

            if(!_activeGroundGO.Contains(releasingGO)){

                Debug.LogError("You are trying to release an object that is not inside of active game objects");
                return;

            }

            pooler.ReleaseObject(releasingGO);

        }

        private void _moveGround(){

            foreach (GameObject ground in _activeGroundGO)
            {
                
                ground.transform.position += Vector3.back * _levelSpeed * Time.deltaTime; 

            }

        }

        //private void 

    }

}
