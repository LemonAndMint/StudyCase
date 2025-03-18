using System.Collections;
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
        private List<Rigidbody> _activeGroundRB; 
        private float _levelSpeed;

        private void Start() {
            
            if(pooler != null)
                pooler.Init(gameManager.LevelData.planePrefbs);

            _levelSpeed = gameManager.LevelData.LevelForwardSpeed;

            _activeGroundRB = new List<Rigidbody>();

            _initLevel();

        }

        void FixedUpdate()
        {
            _moveGround();

        }

        public void DestroyGround(GameObject releasingGO){

            if( releasingGO.TryGetComponent(out Rigidbody releasingRB) && !_activeGroundRB.Contains(releasingRB) ){

                Debug.LogError("You are trying to release an object that is not inside of the active game object list.");
                return;

            }

            _activeGroundRB.Remove(releasingRB);
            pooler.ReleaseObject(releasingRB);

            _activeGroundRB.Add(RetrieveGround());

        }

        public Rigidbody RetrieveGround(){

            Rigidbody rb = pooler.GetObject();
            rb.MovePosition(transform.position);

            return rb;

        }

        private void _moveGround(){

            foreach (Rigidbody groundRB in _activeGroundRB)
            {
                
                groundRB.MovePosition(groundRB.position + Vector3.back * _levelSpeed * Time.deltaTime); 

            }

        }

        private void _initLevel(){

            int counter = gameManager.LevelData.GroundCount;

            for (int i = 1; i < counter + 1; i++)
            {
                Rigidbody groundRB = RetrieveGround();
                groundRB.MovePosition(transform.position - transform.forward * groundRB.transform.localScale.z * i);
                _activeGroundRB.Add(groundRB);

            }

        }

    }

}
