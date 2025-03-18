using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
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
        private List<Transform> _activeGroundTRList; 
        private float _levelSpeed;
        private bool _isGameStarted = false;

        private void Start() {
            
            if(pooler != null)
                pooler.Init(gameManager.LevelData.planePrefbs);

            _levelSpeed = gameManager.LevelData.LevelForwardSpeed;

            _activeGroundTRList = new List<Transform>();

        }

        void FixedUpdate()
        {
            _moveGround();

        }

        public void DestroyGround(GameObject releasingGO){

            if( releasingGO.TryGetComponent(out Transform releasingTR) && !_activeGroundTRList.Contains(releasingTR) ){

                Debug.LogError("You are trying to release an object that is not inside of the active game object list.");
                return;

            }

            _activeGroundTRList.Remove(releasingTR);
            pooler.ReleaseObject(releasingTR);

            _activeGroundTRList.Add(RetrieveGround());

        }

        public Transform RetrieveGround(){

            Transform TR = pooler.GetObject();
            TR.transform.position = transform.position + Vector3.forward * _activeGroundTRList.Last().localScale.z; //#TODO make better positioning

            return TR;

        }

        public void InitLevel(){

            if(_isGameStarted){

                Debug.LogError("You have already initialized the level");
                return;

            }

            int counter = gameManager.LevelData.GroundCount;

            Transform groundTR = pooler.GetObject();
            groundTR.transform.position = transform.position;

            _activeGroundTRList.Add(groundTR);

            for (int i = 1; i < counter + 1; i++)
            {
                groundTR = RetrieveGround();
                Transform newlyAddedTR = _activeGroundTRList.Last();

                groundTR.transform.position = newlyAddedTR.position + Vector3.back * groundTR.transform.localScale.z; 
                // No need to create jump variaties, make it with adding long-short grounds.

                _activeGroundTRList.Add(groundTR);

            }

            // We are instantiating the grounds from further to back. 
            // First added ground should be in last list in order us to determine next ground's position. 
            // Newly repositioned ground's position should be based on its back member's length . 
            // To do this for only one time we need our first instantiated piece to be in last slot.

            groundTR = _activeGroundTRList.First();
            _activeGroundTRList.RemoveAt(0);

            _activeGroundTRList.Add(groundTR);

            _isGameStarted = true;

        }

        public void FlipStartedSwitch(){

            _isGameStarted = false;

        }

        private void _moveGround(){

            foreach (Transform groundTR in _activeGroundTRList)
            {
                
                groundTR.transform.position = groundTR.position + Vector3.back * _levelSpeed * Time.deltaTime; 

            }

        }

    }

}
