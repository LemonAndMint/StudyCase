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
        private ObjectPool<Rigidbody> _groundPool;
        public ObjectPool<Rigidbody> GroundPool{ get=> _groundPool; }

        //Translate GameObject from Rigidbody
        public void Init(List<GameObject> PrefbsToIns) {
            
            _prefbsToIns = PrefbsToIns;
            _groundPool = new ObjectPool<Rigidbody>(_onCreate, _onTake, _onReturn, null, true, 10, 100);
            
        }

        public Rigidbody GetObject(){
            
            return _groundPool.Get();

        }

        public void ReleaseObject(Rigidbody objectToRelease){

            _groundPool.Release(objectToRelease);

        }

        private Rigidbody _onCreate(){

            GameObject groundGO = Instantiate(_prefbsToIns[UnityEngine.Random.Range(0, _prefbsToIns.Count)], Vector3.zero, Quaternion.identity);
            groundGO.name = "Ground " + _groundPool.CountAll;

            if(groundGO.TryGetComponent(out Rigidbody groundRB)){

                return groundRB;

            }

            Debug.LogAssertion("No Rigidbody found in PREFAB... Dont worry! i will add rigidbody for you this time but dont forget to add it in your prefab next time :)");
            return groundGO.AddComponent<Rigidbody>();

        }

        private void _onTake(Rigidbody groundPrefb){

            groundPrefb.gameObject.SetActive(true);

        }

        private void _onReturn(Rigidbody groundPrefb){

            groundPrefb.gameObject.SetActive(false);

        }

    }

}
