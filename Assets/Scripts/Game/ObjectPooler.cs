using System;
using System.Collections.Generic;
using System.Linq;
using Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace Level
{
    public class ObjectPooler : MonoBehaviour
    {
        private List<GameObject> _prefbsToIns;
        private RandomAccessPool<Transform> _groundPool;
        public RandomAccessPool<Transform> GroundPool{ get=> _groundPool; }

        // Translate GameObject from Transform
        public void Init(List<GameObject> PrefbsToIns, int warmUpObjectCount) {
            
            _prefbsToIns = PrefbsToIns;
            _groundPool = new RandomAccessPool<Transform>(_onCreate, _onTake, _onReturn, null, true, 20, 100);

            //WARM UP ++

            Transform[] tempTransArr = new Transform[warmUpObjectCount]; 

            for (int i = 0; i < warmUpObjectCount; i++)
            {
                
                Transform tempTrans = _groundPool.Get();
                tempTransArr[i] = tempTrans;

            }

            foreach (Transform tempTrans in tempTransArr)
            {

                _groundPool.Release(tempTrans);
                
            }

            //WARM UP --
            
        }

        public Transform GetObject(){
            
            return _groundPool.Get();

        }

        public void ReleaseObject(Transform objectToRelease){

            _groundPool.Release(objectToRelease);

        }

        private Transform _onCreate(){

            GameObject groundGO = Instantiate(_prefbsToIns[UnityEngine.Random.Range(0, _prefbsToIns.Count)], Vector3.zero, Quaternion.identity);
            groundGO.name = "Ground " + _groundPool.CountAll;

            if(groundGO.TryGetComponent(out Transform groundRB)){

                return groundRB;

            }

            Debug.LogAssertion("Well i don't know how did you manage to NOT add a TRANSFORM... I would like to know that. Still, i can't allow an object to not have one of it's basic properties!");
            return groundGO.AddComponent<Transform>();

        }

        private void _onTake(Transform groundPrefb){

            groundPrefb.gameObject.SetActive(true);

        }

        private void _onReturn(Transform groundPrefb){

            groundPrefb.gameObject.SetActive(false);

        }

    }

}
