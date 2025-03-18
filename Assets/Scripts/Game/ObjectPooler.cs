using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Level
{
    public class ObjectPooler : MonoBehaviour
    {
        private List<GameObject> _prefbsToIns;
        private ObjectPool<Transform> _groundPool;
        public ObjectPool<Transform> GroundPool{ get=> _groundPool; }

        // Translate GameObject from Transform
        public void Init(List<GameObject> PrefbsToIns) {
            
            _prefbsToIns = PrefbsToIns;
            _groundPool = new ObjectPool<Transform>(_onCreate, _onTake, _onReturn, null, true, 10, 100);
            
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

            Debug.LogAssertion("No Transform found in PREFAB... Dont worry! i will add Transform for you this time but dont forget to add it in your prefab next time :)");
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
