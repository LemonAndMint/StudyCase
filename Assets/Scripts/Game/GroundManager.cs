using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            Rigidbody groundRB = RetrieveGround();
            _activeGroundRB.Add(groundRB);

            for (int i = 1; i < counter + 1; i++)
            {
                groundRB = RetrieveGround();
                Rigidbody newlyAddedRB = _activeGroundRB.Last();

                groundRB.MovePosition( newlyAddedRB.position + Vector3.back * groundRB.transform.localScale.z ); 
                //No need to create jump variaties, make it with adding long-short grounds.

                _activeGroundRB.Add(groundRB);

            }

        }

    }

}
