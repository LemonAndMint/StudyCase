using System;
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

        private void Awake() {
            
            if(pooler != null)
                pooler.Init(gameManager.LevelData.planePrefbs, gameManager.LevelData.GroundCount * 2);

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

            _activeGroundTRList.Add(CreateGround());

            _activeGroundTRList.Remove(releasingTR);
            pooler.ReleaseObject(releasingTR);

        }

        public Transform CreateGround(){

            Transform TR = pooler.GetObject();

            float distance = _lengthBetweenGrounds(_activeGroundTRList.Last().localScale.z, TR.localScale.z);

            TR.transform.position = _activeGroundTRList.Last().transform.position + Vector3.forward * distance; 

            return TR;

        }

        private float _lengthBetweenGrounds(float prevObjLength, float currObjLength){

            float normalDistance = prevObjLength / 2 - currObjLength / 2;

            if(normalDistance < 0){

                return currObjLength + gameManager.LevelData.MinDistBetweenGrounds;

            }

            if(normalDistance > gameManager.LevelData.MaxDistBetweenGrounds){

                return gameManager.LevelData.MaxDistBetweenGrounds;

            }

            return prevObjLength;

        }
        public void InitLevel(){

            if(gameManager.isGameStarted){

                Debug.LogError("You have already initialized the level");
                return;

            }

            int counter = gameManager.LevelData.GroundCount;

            Transform groundTR = pooler.GetObject();
            groundTR.transform.position = transform.position;

            _activeGroundTRList.Add(groundTR);

            for (int i = 1; i < counter; i++)
            {
                groundTR = CreateGround();
                Transform newlyAddedTR = _activeGroundTRList.Last();

                float distance = _lengthBetweenGrounds(newlyAddedTR.localScale.z, groundTR.localScale.z);

                groundTR.transform.position = newlyAddedTR.position + Vector3.back * distance; 
                // No need to create jump variaties, make it with adding long-short grounds.

                _activeGroundTRList.Add(groundTR);

            }

            // We are instantiating grounds from further to back. 
            // First added ground should be in last in list in order us to determine next ground's position. 
            // Newly repositioned ground's position should be based on its back member's length. 
            // To do this, only one time, we need our first instantiated piece to be in last slot.

            groundTR = _activeGroundTRList.First();
            _activeGroundTRList.RemoveAt(0);

            _activeGroundTRList.Add(groundTR);

        }

        public void ClearLevel(){

            _activeGroundTRList.ForEach( x => pooler.ReleaseObject(x) );
            _activeGroundTRList.RemoveRange(0, _activeGroundTRList.Count);

        }

        private void _moveGround(){

            foreach (Transform groundTR in _activeGroundTRList)
            {
                groundTR.transform.position = groundTR.position + Vector3.back * _levelSpeed * Time.deltaTime; 

            }

        }

    }

}
